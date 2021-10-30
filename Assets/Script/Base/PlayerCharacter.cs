using System.Collections;
using Assets.Script.scriptableobject;
using Assets.Script.scriptableobject.Character;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Script.Base
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSO PlayerCharacterSo;

        private string Name;
        public int Hp;
        public int Atk;
        public float Speed;
        private GameObject Popup;
        private Animator animator;
     
        public void Start()
        {
            Name = PlayerCharacterSo.Name;
            Hp = PlayerCharacterSo.MaxHp;
            Atk = PlayerCharacterSo.Atk;
            Speed = PlayerCharacterSo.Speed;
            Popup = PlayerCharacterSo.Popup;
            animator = GetComponent<Animator>();
        }

        public void PrintAll()
        {
            Debug.Log($"name:{Name}");
            Debug.Log($"HP:{Hp}");
            Debug.Log($"ATK:{Atk}");
            Debug.Log($"Speed:{Speed}");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyHitBox"))
            {
                var enemyCharacter = other.GetComponentInParent<EnemyCharacter>();
                Hp -= enemyCharacter.Atk;
                ShowPopUp(enemyCharacter.Atk);
                if (Hp <= 0)
                {
                    animator.SetBool("Dead",true);
                    StartCoroutine(AnimatorDead());
                }
                Debug.Log($"{Name} have : {Hp}");
            }
        }

        private void ShowPopUp(int dmg)
        {
            var spawnPopup = Instantiate(Popup,transform.position,Quaternion.identity,transform);
            var textMesh = spawnPopup.GetComponent<TextMesh>();
            textMesh.text = $"{dmg}";
            textMesh.color = Color.red;
        }
        
        IEnumerator AnimatorDead()
        {
            yield return new WaitForSeconds(1.517f);
            
        }
        
    }
}
