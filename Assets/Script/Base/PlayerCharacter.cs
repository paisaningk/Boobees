using System.Collections;
using Assets.Script.Controller;
using Assets.Script.scriptableobject.Character;
using Assets.scriptableobject.Item;
using UnityEngine;

namespace Assets.Script.Base
{
    public class PlayerCharacter : MonoBehaviour 
    {
        [SerializeField] public AdsManager adsManager;

        [SerializeField] private CharacterSO PlayerCharacterSo;
        public ItemSO[] ItemSo;
        private PlayerController playerController;
        private string Name;
        public int Hp;
        public int Atk;
        public int Gold = 0;
        public float Speed;
        public float DashCd;
        public float CritAtk;
        public float CritRate;
        private GameObject Popup;
        private Animator animator;

        public void Awake()
        {
            Name = PlayerCharacterSo.Name;
            Hp = PlayerCharacterSo.MaxHp;
            Atk = PlayerCharacterSo.Atk;
            Speed = PlayerCharacterSo.Speed;
            Popup = PlayerCharacterSo.Popup;
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();

            //PrintAll();
        }

        public void Update()
        {
           // Debug.Log(Gold);
        }

        public void PrintAll()
        {
            //Debug.Log("Enemy");
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
                    animator.SetBool("Dead", true);
                    StartCoroutine(Dead());
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

        IEnumerator Dead()
        {
            yield return new WaitForSeconds(2);
            Debug.Log("dead it work");
            playerController.Dead();
        }
    }
}
