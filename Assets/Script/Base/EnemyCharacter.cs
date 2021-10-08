using Assets.Script.scriptableobject;
using Assets.Script.scriptableobject.Character;
using UnityEngine;

namespace Assets.Script.Base
{
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSO EnemyCharacterSo;

        private string Name;
        private int Hp;
        public int Atk;
        private float Speed;
        
        public void Start()
        {
            Name = EnemyCharacterSo.Name;
            Hp = EnemyCharacterSo.MaxHp;
            Atk = EnemyCharacterSo.Atk;
            Speed = EnemyCharacterSo.Speed;
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
                //other.gameObject.GetComponent<PlayerCharacter>();
                var atkPlayer = GameObject.Find("Ronin Player").GetComponent<PlayerCharacter>();
                Hp -= atkPlayer.Atk;
                if (Hp <= 0)
                {
                    //gameObject.SetActive(false); 
                    Destroy(this.gameObject);
                }
                Debug.Log($"{Name} have : {Hp}");
            }
        }
        
        
    }
}
