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
        private float playerCritRate = 10f;
        private GameObject Popup;
        public void Start()
        {
            Name = EnemyCharacterSo.Name;
            Hp = EnemyCharacterSo.MaxHp;
            Atk = EnemyCharacterSo.Atk;
            Speed = EnemyCharacterSo.Speed;
            Popup = EnemyCharacterSo.Popup;
            
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
                var critPercentRand = Random.Range(1, 101);
                
                if (critPercentRand <= playerCritRate)
                {
                    var atkCrit = atkPlayer.Atk * 2;
                    ShowPopUpCrit(atkCrit);
                    Hp -= atkCrit;
                }
                else
                {
                    ShowPopUp(atkPlayer.Atk);
                    Hp -= atkPlayer.Atk;
                }
                
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
            playerController.knockback = false;
            var knockbackForce = 300;
            Vector2 difference = (Rb.transform.position - other.transform.position).normalized;
            Vector2 force = difference * knockbackForce;
            var raycastHit2D = Physics2D.Raycast(transform.position,difference,knockbackForce,knockbackLayerMask);
            if (raycastHit2D.collider != null) force = raycastHit2D.point;
            
            Rb.AddForce(force,ForceMode2D.Impulse);
        }
        
        private void ShowPopUp(int dmg)
        {
            var spawnPopup = Instantiate(Popup,transform.position,Quaternion.identity,transform);
            var textMesh = spawnPopup.GetComponent<TextMesh>();
            textMesh.text = $"{dmg}";
            textMesh.color = Color.yellow;
        }
        
        private void ShowPopUpCrit(int dmg)
        {
            var spawnPopup = Instantiate(Popup,transform.position,Quaternion.identity,transform);
            var textMesh = spawnPopup.GetComponent<TextMesh>();
            textMesh.text = $"{dmg}";
            textMesh.color = Color.white;
        }
        
    }
}
