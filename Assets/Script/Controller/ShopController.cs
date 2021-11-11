using System.Collections;
using Assets.Script.Base;
using Assets.scriptableobject.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Controller
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Transform[]  SpawnPoint;
        [SerializeField] private GameObject[] Common;
        [SerializeField] private GameObject[] Uncommon;
        [SerializeField] private GameObject[] Rare;
        [SerializeField] private GameObject[] Epic;
        [SerializeField] private GameObject[] Cursed;
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject EPopup;
        [SerializeField] private Button backButton;
        [SerializeField] private Button rngButton;
        [SerializeField] private Button healButton;
        private bool Shop = false;
        private Collider2D collider2D;
        private Playerinput playerInput;
        private Collider2D player;
        
        
        public void Start()
        { 
            RngItemandSpawn();
            playerInput = new Playerinput();
            playerInput.PlayerAction.Buy.performed += context => Talk();
            
            rngButton.onClick.AddListener(DeleteItem);
            backButton.onClick.AddListener(Back);
            healButton.onClick.AddListener(Heal);
            
            playerInput.Enable();
        }

        private void Talk()
        {
            if (Shop == true)
            {
                shopMenu.SetActive(true);
            }
            else
            {
                Back();
            }
        }

        private void Back()
        {
            shopMenu.SetActive(false);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                player = other;
                Shop = true;
                EPopup.SetActive(true);
            }
        }

        private void OnTriggerExit2D()
        {
            Shop = false;
            EPopup.SetActive(false);
            Back();
        }

        public void DeleteItem()
        {
            var iteminscene = GameObject.FindGameObjectsWithTag("Item");
            foreach (var item in iteminscene)
            {
                Destroy(item);
            }
            RngItemandSpawn();
        }

        public void Heal()
        {
            var playerCharacter= player.GetComponent<PlayerCharacter>();
            var heal50= playerCharacter.MaxHp / 2;
            playerCharacter.Hp += heal50;
            if (playerCharacter.Hp >= playerCharacter.MaxHp)
            {
                playerCharacter.Hp = playerCharacter.MaxHp;
            }
        }

        public void RngItemandSpawn()
        {
            foreach (var t in SpawnPoint)
            {
                var rngTier = Random.Range(1 , 156);
                if (rngTier <= 68)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Common[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 114)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Uncommon[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 138)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Rare[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 149)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Epic[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 155)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Cursed[Rngitem], t.position ,Quaternion.identity);
                }
            }
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(3);
            var rngTier = Random.Range(1 , 156);
            if (rngTier <= 68)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 68");
            }
            else if (rngTier <= 114)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 114");
            }
            else if (rngTier <= 138)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 138");
            }
            else if (rngTier <= 149)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 149");
            }
            else if (rngTier <= 155)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 155");
            }
            StartCoroutine(Test());
        }
    }
}
