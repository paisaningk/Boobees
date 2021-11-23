using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = 5f;
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
        var a = (time -= Time.deltaTime) / 60;
        Debug.Log($"deltaTime = {Time.deltaTime}");
        Debug.Log(a);
    }
    
}
