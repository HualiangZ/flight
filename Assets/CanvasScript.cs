using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Drawing;

public class PointerScript : MonoBehaviour
{
    public TMP_Text warning;
    public Flight player;
    public float y;
    public float x;
    public GameObject crosshairPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y = player.pitch;
        x = player.yaw;
        crosshairPoint.transform.localPosition = new Vector3(player.yaw * 30, player.pitch * 30, 0);
        if (player.gForceCountDown < 10f)
        {
            warning.text = "HIGH G FORCE";
        }
        else if(player.isSleeping && player.sleepTimer < 15f)
        {
            warning.text = "Sleeping: " + player.sleepTimer;
        }
        else
        {
            warning.text = "";
        }
    }
}
