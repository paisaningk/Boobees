using Assets.Script.Controller;
using Assets.Script.scriptableobject;
using Assets.Script.scriptableobject.Character;
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
        public void Start()
        {
            Name = EnemyCharacterSo.Name;
            Hp = EnemyCharacterSo.MaxHp;
            Atk = EnemyCharacterSo.Atk;
            Speed = EnemyCharacterSo.Speed;
            //PrintAll();
            Rb = GetComponent<Rigidbody2D>();
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
                //other.gameObject.GetComponent<PlayerCharacter>();
                var atkPlayer = GameObject.Find("Ronin Player").GetComponent<PlayerCharacter>();
                Hp -= atkPlayer.Atk;
                if (Hp <= 0)
                {
                    //gameObject.SetActive(false); 
                    Destroy(this.gameObject);
                }

                Knockback(other);
                Debug.Log($"{Name} have : {Hp}");
            }
        }

        private void Knockback(Collider2D other)
        {
            var KnockbackDirection = 2000f;
            var moveDirectionPush = Rb.transform.position - other.transform.position;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,moveDirectionPush,KnockbackDirection,
                knockbackLayerMask);
            if (raycastHit2D.collider != null)
            {
                moveDirectionPush = raycastHit2D.point;
            }
            Rb.AddForce(moveDirectionPush.normalized * KnockbackDirection);
        }
        
    }
}
