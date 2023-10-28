using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{

    public Rotation player;
    public float y;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y = player.pitch;
        x = player.yaw;
        transform.localPosition = new Vector3(player.yaw * 30, player.pitch * 30, 0);
    }
}
