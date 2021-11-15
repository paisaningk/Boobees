using System.Collections;
using System.Collections.Generic;
using Assets.Script.Base;
using Script.Controller;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed;
    public int DMG;

    private Transform player;
    private Vector2 target;

    private PlayerController playerController;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    }
    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            Destroy(gameObject);
        }
    }
}

    
