using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float engineSpeedInterval = 0.5f;
    [SerializeField] private Rigidbody m_Rigidbody;

    RaycastHit hit;

    //public float speed = 20f;
    private float lift = 0f;
    public float zVelocity;
    public float xVelocity;
    public float engineSpeed;
    public float maxEngineSpeed = 500;
    public float liftPower = 0.5f; //made up number
    public float yawSpeed = 0.1f;
    public float pitchSpeed = 0.1f;

    public float yaw;
    public float pitch;
    public float roll;
    private float limit = 1f;
    private float plainFlap = 0;
    
    // Update is called once per frame

    void Update()
    {
        zVelocity = m_Rigidbody.velocity.z;
        xVelocity = m_Rigidbody.velocity.x;
        flaps();
        
        LiftUpdate();
    }

    private void FixedUpdate()
    {
        if(engineSpeed < 0f) 
            engineSpeed = 0f;

        if(yaw > limit)
            yaw = limit;

        if (yaw < -limit)
            yaw = -limit;

        if (pitch > limit)
            pitch = limit;

        if (pitch < -limit)
            pitch = -limit;

        planeInput();
        m_Rigidbody.AddForce(transform.forward * maxEngineSpeed * engineSpeed);
        m_Rigidbody.AddTorque(transform.up * yaw * response());
        m_Rigidbody.AddTorque(-transform.right * pitch * response());
        m_Rigidbody.AddTorque(-transform.forward * roll * response());

        m_Rigidbody.AddForce(transform.up * lift);
    }

    private float response()
    {
        return m_Rigidbody.mass * 2 / 10f;
    }

    //Lift force
    private void LiftUpdate()
    {
        float angleOfAttack = Vector3.Dot(m_Rigidbody.velocity.normalized, transform.forward) + plainFlap;
        lift = (Mathf.Pow(zVelocity+xVelocity, 2) / 2) * angleOfAttack * liftPower;
        
    }

    //deploy plain flaps if plane is lower then 5(unity units) form the ground
    //plainFlap = 1 will increase lift without increase angle of attack
    //plainflap = -1 will decrease lift without decrease angle of attack
    private void flaps()
    {
        Debug.Log(Physics.Raycast(transform.position, Vector3.down, out hit));


        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //Debug.Log(hit.distance<5);
            plainFlap = 0;
            if (hit.distance < 5)
                plainFlap = 1;
        }

        if ((Input.GetKey(KeyCode.G)))
            plainFlap = -1;
    }

    private void planeInput()
    { 
        yaw += yawSpeed * Input.GetAxis("Mouse X");
        pitch += pitchSpeed * Input.GetAxis("Mouse Y");
        roll = Input.GetAxis("Horizontal");


        if (Input.GetKey(KeyCode.Space))
        {
            if (engineSpeed < maxEngineSpeed)
                engineSpeed += engineSpeedInterval;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (engineSpeed > 0f)
                engineSpeed -= engineSpeedInterval;
        }

       
    }
}
