using Assets.Script.scriptableobject;
using Assets.Script.scriptableobject.Character;
using Script.Controller;
using UnityEngine;

namespace Assets.Script.Base
{
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSO EnemyCharacterSo;
        [SerializeField] private LayerMask knockbackLayerMask;
        private string Name;
        private int Hp;
        public int Atk;
        private float Speed;
        private Rigidbody2D Rb;
        private PlayerController playerController;
        public void Start()
        {
            Name = EnemyCharacterSo.Name;
            Hp = EnemyCharacterSo.MaxHp;
            Atk = EnemyCharacterSo.Atk;
            Speed = EnemyCharacterSo.Speed;
            //PrintAll();
            Rb = GetComponent<Rigidbody2D>();
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public void PrintAll()
        {
            Debug.Log("Enemy");
            Debug.Log($"name:{Name}");
            Debug.Log($"HP:{Hp}");
            Debug.Log($"ATK:{Atk}");
            Debug.Log($"Speed:{Speed}");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerHitBox"))
            {
                var atkPlayer = other.GetComponentInParent<PlayerCharacter>();
                Hp -= atkPlayer.Atk;
                if (Hp <= 0)
                {
                    Destroy(this.gameObject);
                }
                if (playerController.knockback == true)
                {
                    Knockback(other);
                }
            }
        }

        private void Knockback(Collider2D other)
        {
            // var knockbackDirection = 400;
            // var moveDirectionPush = Rb.transform.position - other.transform.position * knockbackDirection;
            // var raycastHit2D = Physics2D.Raycast(transform.position, moveDirectionPush, knockbackDirection,
            //     knockbackLayerMask);
            // if (raycastHit2D.collider != null) moveDirectionPush = raycastHit2D.point;
            // Rb.MovePosition(moveDirectionPush.normalized);
            
            playerController.knockback = false;
            var knockbackForce = 300;
            Vector2 difference = (Rb.transform.position - other.transform.position).normalized;
            Vector2 force = difference * knockbackForce;
            var raycastHit2D = Physics2D.Raycast(transform.position,difference,knockbackForce,knockbackLayerMask);
            if (raycastHit2D.collider != null) force = raycastHit2D.point;
            
            Rb.AddForce(force,ForceMode2D.Impulse);
        }
        
    }
}
