using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoping());
    }
    
    
    IEnumerator Shoping()
    {
        Debug.Log("eeee");
        yield return new WaitForSeconds(3);
        Debug.Log("adc");
    }
}
