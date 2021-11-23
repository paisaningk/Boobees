using System.Collections;
using Assets.Script.scriptableobject.Character;
using Assets.scriptableobject.Item;
using Script.Controller;
using UnityEngine;
using Sound;

namespace Assets.Script.Base
{
    public class PlayerCharacter : MonoBehaviour 
    {
        [SerializeField] public AdsManager adsManager;

        [SerializeField] private CharacterSO PlayerCharacterSo;
        public ItemSO[] ItemSo;
        private PlayerController playerController;
        public string Name;
        public float MaxHp;
        public float Hp;
        public int Atk;
        public int Gold = 0;
        public float Speed;
        public float DashCd;
        public int CritAtk = 1;
        public int CritRate = 2;
        private GameObject Popup;
        private Animator animator;

        public void Awake()
        {
            Name = PlayerCharacterSo.Name;
            Hp = PlayerCharacterSo.MaxHp;
            MaxHp = PlayerCharacterSo.MaxHp;
            Atk = PlayerCharacterSo.Atk;
            Speed = PlayerCharacterSo.Speed;
            Popup = PlayerCharacterSo.Popup;
            animator = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>();
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
                SoundManager.Instance.Play(SoundManager.Sound.PlayerTakeHit1);
                var enemyCharacter = other.GetComponentInParent<EnemyCharacter>();
                Hp -= enemyCharacter.Atk;
                ShowPopUp(enemyCharacter.Atk);
                if (Hp <= 0)
                {
                    animator.SetBool("Dead", true);
                    playerController.Dead();
                }
            }

            if (other.CompareTag("Projectile"))
            {
                SoundManager.Instance.Play(SoundManager.Sound.PlayerTakeHit1);
                var arrow = other.GetComponent<Arrow>();
                Hp -= arrow.DMG;
                ShowPopUp(arrow.DMG);
                if (Hp <= 0)
                {
                    animator.SetBool("Dead", true);
                    playerController.Dead();
                }
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
            
        }
    }
}
