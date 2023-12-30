using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Flight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float engineSpeedInterval = 0.5f;
    [SerializeField] private Rigidbody m_Rigidbody;

    RaycastHit hit;

    
    public float zVelocity;
    public float xVelocity;
    public float engineSpeed;
    public float liftPower = 1.5f; //made up number
    public float yawSpeed = 0.1f;
    public float pitchSpeed = 0.1f;
    public float altitudeZ;

    public float lift;
    public float yaw;
    public float pitch;
    private float roll;
    private float initLift;
    public float gForceCountDown = 10f;
    public float sleepTimer = 15f;
    public bool isSleeping = false;

    // Update is called once per frame

    void Update()
    {
        altitudeZ = gameObject.transform.localEulerAngles.z;

        zVelocity = m_Rigidbody.velocity.z;
        xVelocity = m_Rigidbody.velocity.x;
        InitLift();
        LiftUpdate();
        SetSleep();
        GForce();
        if (isSleeping == false)
        {
            PlaneInput();
        }

/*        transform.rotation = Input.gyro.attitude;
        Debug.Log(transform.rotation);*/

        //Debug.Log(gForceCountDown);
    }

    private void FixedUpdate()
    {
        
        if(engineSpeed < 0f) 
            engineSpeed = 0f;

        if(yaw > 1f)
            yaw = 1f;

        if (yaw < -1f)
            yaw = -1f;

        if (pitch > 1f)
            pitch = 1f;

        if (pitch < -1f)
            pitch = -1f;

        
        m_Rigidbody.AddForce(transform.forward * engineSpeed * engineSpeed);
        m_Rigidbody.AddTorque(transform.up * yaw * 500f);
        m_Rigidbody.AddTorque(-transform.right * pitch * 500f);
        m_Rigidbody.AddTorque(-transform.forward * roll * 500f);

        m_Rigidbody.AddForce(transform.up * lift);
    }

    //Lift force
    private void LiftUpdate()
    {
        lift = (Mathf.Pow(System.Math.Abs(zVelocity) + System.Math.Abs(xVelocity), 2f) / 2f)
            * initLift * liftPower;
        
    }

    //deploy plain flaps if plane is lower then 5(unity units) form the ground
    //initLift = 2 will increase lift to help plane get of the ground
    private void InitLift()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //Debug.Log(hit.distance<5);
            initLift = 0f;
            if (hit.distance < 5f)
                initLift = 2f;
        }

    }

    public Boolean GForce()
    {
        if((engineSpeed > 600f && yaw > 0.8f) || (engineSpeed > 600f &&yaw < -0.8f) || 
            (engineSpeed > 600f && pitch > 0.8f))
        {
            Debug.Log(gForceCountDown);
            gForceCountDown -= Time.deltaTime;
            if(gForceCountDown < 0f) 
            {
                yaw = 0f;
                pitch = 0f;
                isSleeping = true;
                return true;
            }
        }
        else
        {
            gForceCountDown = 10f;
            return false;       
        }
        
        return false;
    }

    private void SetSleep()
    {
        if (isSleeping)
        {
            engineSpeed -= 0.1f;
            sleepTimer -= Time.deltaTime; 
            if (sleepTimer < 0f)
            {
                isSleeping = false;
                sleepTimer = 15f;
            }
        }
    }

    private void PlaneInput()
    { 
        yaw += yawSpeed * Input.GetAxis("Mouse X");
        pitch += pitchSpeed * Input.GetAxis("Mouse Y");
        roll = Input.GetAxis("Horizontal");


        if (Input.GetKey(KeyCode.Space))
        {
            if (engineSpeed < 500f)
            {
                engineSpeed += engineSpeedInterval;
            }

                
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (engineSpeed > 0f)
            {
                engineSpeed -= engineSpeedInterval;
            }
             
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(engineSpeed < 700f)
            {
                engineSpeed += engineSpeedInterval;
            }

        }
        else
        {
            if (engineSpeed > 500f)
            {
                engineSpeed -= engineSpeedInterval;
            }
        }
 

    }
    public float Yaw   
    {
        get => yaw;
    }
    public float Pitch
    {
        get => pitch;
    }
    public float Roll
    {
        get => roll;
    }
    public float GForceCountDown
    {
        get => gForceCountDown;
    }
    public float SleepTimer
    {
        get => sleepTimer;
    }
    public Boolean IsSleeping
    {
        get => isSleeping;
    }
    public float AltitudeZ
    {
        get => altitudeZ;
    }
}
