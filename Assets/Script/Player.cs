using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rd;
    private Vector3 MoveDie;
    private const float MoveSpeed = 5f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Rd = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        float MoveX = 0f;
        float MoveY = 0f;
        
        Rd.MoveRotation(0);

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
        
        animator.SetFloat("MoveX",MoveX);
        animator.SetFloat("MoveY",MoveY);
        animator.SetFloat("Speed",MoveDie.sqrMagnitude);
        
    }

    private void FixedUpdate()
    {
        Rd.velocity = MoveDie * MoveSpeed;
    }
}
