using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private float destroyTime = 3f;
    private Vector3 offSet = new Vector3(0,1f,0);
    void Start()
    {
        Destroy(this.gameObject , destroyTime);
        transform.localPosition += offSet;
    }

    // Update is called once per frame
    
}
