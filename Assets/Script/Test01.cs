using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = 5f;
    private float cccc = 0;
    private float dddd;
    void Start()
    {
        //adc();
    }

    private void Update()
    {
        if (time >= 0)
        {
            adc();
        }
        
    }

    private void adc()
    {
        var dddd = (time -= Time.deltaTime) / 60;
        Debug.Log($"deltaTime = {Time.deltaTime}");
        Debug.Log(dddd);
    }
    
}
