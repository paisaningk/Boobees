using Script.Controller;
using Script.Spawn;
using UnityEngine;

namespace Script.Base
{
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
            if (SpawnWave.CurrentWaveNumber >= 5)
            {
                DMG += 10;
            }

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
}

    
