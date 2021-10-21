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
        private bool knockbacking = false;
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
            var KnockbackDirection = 400;
            var moveDirectionPush = Rb.transform.position - other.transform.position * KnockbackDirection;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,moveDirectionPush,KnockbackDirection,
                knockbackLayerMask);
            if (raycastHit2D.collider != null)
            {
                moveDirectionPush = raycastHit2D.point;
            }
            Rb.MovePosition(moveDirectionPush.normalized);
            playerController.knockback = false;
        }
        
    }
}
