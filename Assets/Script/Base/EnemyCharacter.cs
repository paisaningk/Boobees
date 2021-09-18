using Assets.Script.scriptableobject;
using UnityEngine;

namespace Assets.Script.Base
{
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSO enemyCharacterSo;
    
        private string Name;
        private int Hp;
        public int Atk;
        private float Speed;
        
        public void Start()
        {
            Name = enemyCharacterSo.Name;
            Hp = enemyCharacterSo.MaxHp;
            Atk = enemyCharacterSo.Atk;
            Speed = enemyCharacterSo.Speed;
            //PrintAll();
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
                var atkPlayer = GameObject.Find("Player").GetComponent<PlayerCharacter>();
                Hp -= atkPlayer.Atk;
                if (Hp <= 0)
                {
                    gameObject.SetActive(false); 
                }
                Debug.Log($"{Name} have : {Hp}");
            }
        }
    }
}
