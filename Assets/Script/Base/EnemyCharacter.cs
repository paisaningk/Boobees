using System;
using System.Collections;
using Assets.Script.scriptableobject.Character;
using Script.Controller;
using Script.Pickup;
using Script.Sound;
using Script.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Base
{
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] public CharacterSO[] EnemyCharacterSo;
        [SerializeField] private LayerMask knockbackLayerMask;
        [SerializeField] private GameObject GoldPrefab;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private bool isBoss;
        private string Name;
        public int Hp;
        public int Atk;
        public float Speed;
        private Rigidbody2D Rb;
        private PlayerCharacter player;
        private float playerCritRate;
        private GameObject Popup;
        private Vector3 offset;
        private SpriteRenderer spriteRenderer;
        public bool isDeadForBoss = false;
        private int wave = 1;
        

        public void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
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
                    StartCoroutine(Setcoloattack());
                }
                else
                {
                    ShowPopUp(atkPlayer.Atk);
                    Hp -= atkPlayer.Atk;
                    StartCoroutine(Setcoloattack());
                }
                
                if (Hp <= 0)
                {
                    if (isBoss == true)
                    {
                        SoundManager.Instance.Play(SoundManager.Sound.EnemyTakeHit);
                        isDeadForBoss = true;
                    }
                    else
                    {
                        SoundManager.Instance.Play(SoundManager.Sound.EnemyTakeHit);
                        StartCoroutine(Deaddelay());
                    }
                    
                }
                
            }
        }

        
        
        IEnumerator Setcoloattack()
        { 
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
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
            //playerController.knockback = false;
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
