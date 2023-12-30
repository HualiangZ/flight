using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderData;
public class AltiMeterScript : MonoBehaviour
{
    public RawImage attitudeIndicator;
    public TMP_Text height;
    public Flight playerZ;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attitudeIndicator.uvRect = new Rect(0, -player.localEulerAngles.x / 360, 1, 1);
        attitudeIndicator.transform.rotation = Quaternion.Euler(0f, 0f, -playerZ.AltitudeZ);
        height.text = "" + (int)Math.Round(player.transform.position.y);
    }
}
