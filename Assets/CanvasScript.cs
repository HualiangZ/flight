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
        y = player.Pitch;
        x = player.Yaw;
        crosshairPoint.transform.localPosition = new Vector3(player.Yaw * 30, player.Pitch * 30, 0);
        if (player.GForceCountDown < 10f)
        {
            warning.text = "HIGH G FORCE";
        }
        else if(player.IsSleeping && player.SleepTimer < 15f)
        {
            warning.text = "Sleeping: " + (int)Math.Round(player.SleepTimer);
        }
        else
        {
            warning.text = "";
        }


    }
}
