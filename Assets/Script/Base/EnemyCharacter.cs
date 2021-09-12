using Assets.Script.scriptableobject;
using UnityEngine;

namespace Assets.Script.Base
{
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSo enemyCharacterSo;
    
        private string name;
        private int hp;
        private int Atk;
        private float Speed;
        
        public void Start()
        {
            name = enemyCharacterSo.Name;
            hp = enemyCharacterSo.MaxHp;
            Atk = enemyCharacterSo.Atk;
            Speed = enemyCharacterSo.Speed;
            PrintAll();
        }

        public void PrintAll()
        {
            Debug.Log("Enemy");
            Debug.Log($"name:{name}");
            Debug.Log($"HP:{hp}");
            Debug.Log($"ATK:{Atk}");
            Debug.Log($"Speed:{Speed}");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("hit");
            hp -= 100;
            if (hp <= 0)
            {
               gameObject.SetActive(false); 
            }
        }
    }
}
