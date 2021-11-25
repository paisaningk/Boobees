using System;
using System.Collections;
using Assets.Script.Pickup;
using Assets.Script.scriptableobject;
using Assets.Script.scriptableobject.Character;
using Script.Controller;
using Script.Spawn;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Sound;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Script.Base
{
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] public CharacterSO EnemyCharacterSo;
        [SerializeField] private LayerMask knockbackLayerMask;
        [SerializeField] private GameObject GoldPrefab;
        [SerializeField] private EnemyType enemyType;
        private string Name;
        public int Hp;
        public int Atk;
        public float Speed;
        private Rigidbody2D Rb;
        private PlayerController playerController;
        private float playerCritRate;
        private GameObject Popup;
        private bool isdead = true;
        private Vector3 offset;
        
        public void Start()
        {
            Name = EnemyCharacterSo.Name;
            Hp = EnemyCharacterSo.MaxHp;
            Atk = EnemyCharacterSo.Atk;
            Speed = EnemyCharacterSo.Speed;
            Popup = EnemyCharacterSo.Popup;
            Rb = GetComponent<Rigidbody2D>();

            if (SpawnWave.CurrentWaveNumber >= 5)
            {
                Hp += 30;
                Atk += 10;
            }
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
                playerCritRate = atkPlayer.CritRate;
                var critPercentRand = Random.Range(1, 101);
                
                if (critPercentRand <= playerCritRate)
                {
                    var atkCrit = atkPlayer.Atk * atkPlayer.CritAtk;
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
                    if (isdead == true)
                    {
                        isdead = false;
                        SoundManager.Instance.Play(SoundManager.Sound.EnemyTakeHit);
                        StartCoroutine(Deaddelay());
                        
                    }
                    
                }
                
            }
        }

        IEnumerator Deaddelay()
        {
            DropGold();
            yield return new WaitForSeconds(0.1f);
            Destroy(this.gameObject);
        }

        private void Knockback(Collider2D other)
        {
            //Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            textMesh.color = Color.white;
        }
        
        private void ShowPopUpCrit(int dmg)
        {
            var spawnPopup = Instantiate(Popup,transform.position,Quaternion.identity,transform);
            var textMesh = spawnPopup.GetComponent<TextMesh>();
            textMesh.text = $"{dmg}";
            textMesh.color = Color.yellow;
        }

        private void DropGold()
        {
            var gold = 0;
            switch (enemyType)
            {
                case EnemyType.Slime:
                    gold = Random.Range(3 , 10);
                    gold /= 2;
                    GoldPrefab.GetComponent<Gold>().goldAmount = gold;
                    break;
                case EnemyType.Ranger:
                    gold = Random.Range(7 , 12);
                    GoldPrefab.GetComponent<Gold>().goldAmount = gold;
                    break;
                case EnemyType.Golem:
                    gold = Random.Range(8 , 12);
                    GoldPrefab.GetComponent<Gold>().goldAmount = gold;
                    break;
                case EnemyType.Charger:
                    gold = Random.Range(12 , 18);
                    GoldPrefab.GetComponent<Gold>().goldAmount = gold;
                    break;
                case EnemyType.Boss:
                    gold = Random.Range(35 , 41);
                    GoldPrefab.GetComponent<Gold>().goldAmount = gold;
                    break;
            }
            Instantiate(GoldPrefab,transform.position, Quaternion.identity);
        }
    }
}
