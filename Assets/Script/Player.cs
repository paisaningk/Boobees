using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rd;
    private Vector3 MoveDie;
    private const float MoveSpeed = 30f;
        
    private void Awake()
    {
        Rd = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        Rd.MoveRotation(0);
        float MoveX = 0f;
        float MoveY = 0f;
        
        if (Input.GetKey(KeyCode.W))
        {
            MoveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveX = +1f;
        }
        
        MoveDie = new Vector3(MoveX,MoveY).normalized;
    }

    private void FixedUpdate()
    {
        Rd.velocity = MoveDie * MoveSpeed;
    }
}
