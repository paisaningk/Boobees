using Assets.Script.Base;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class EnemyController : MonoBehaviour
    {
        private Rigidbody2D Rb;
        private Transform player;
        private float movespeed = 5f;

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            Vector2 direction = player.position - transform.position;
            var directionNormalized = direction.normalized;
            moveCharacter(directionNormalized);
        }
    
        private void moveCharacter(Vector2 diretion)
        {
            Rb.MovePosition((Vector2)transform.position + (diretion * movespeed * Time.deltaTime));
        }
    }
}
