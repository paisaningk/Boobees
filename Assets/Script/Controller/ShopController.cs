using System.Collections;
using Assets.Script.Base;
using Assets.scriptableobject.Item;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private TextMeshProUGUI HealcostText;
        [SerializeField] private TextMeshProUGUI RNGText;
        
        private int HealCost = 20;
        private int RNGCost = 20;
        private bool Shop = false;
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
                string[] talk = new[] {"Hello , You cum again ","Hello nigga" , "you give me a monkey" };
                string[] talka = new[] {"Hello","Hello adc" , "you give me" };
                var range = Random.Range(0, talk.Length);
                shopMenu.SetActive(true);
                Text.text = talka[range];
                HealcostText.text = $"{HealCost}";
                RNGText.text = $"{RNGCost}";
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
        }

        private void DeleteItem()
        {
            var playerCharacter = player.GetComponent<PlayerCharacter>();
            if (playerCharacter.Gold >= RNGCost)
            {
                playerCharacter.Gold -= RNGCost;
                var iteminscene = GameObject.FindGameObjectsWithTag("Item");
                foreach (var item in iteminscene)
                {
                    Destroy(item);
                }
                RngItemandSpawn();
            }
            else
            {
                Text.text = $"GOLD not enough";
            }
        }

        private void Heal()
        {
            var playerCharacter = player.GetComponent<PlayerCharacter>();
            if (playerCharacter.Gold >= HealCost)
            {
                playerCharacter.Gold -= HealCost;
                //var heal50= playerCharacter.MaxHp / 2;
                playerCharacter.Hp += HealCost;
                if (playerCharacter.Hp >= playerCharacter.MaxHp)
                {
                    playerCharacter.Hp = playerCharacter.MaxHp;
                }
            }
            else

            {
                Text.text = $"GOLD not enough";
            }
        }

        private void RngItemandSpawn()
        {
            foreach (var t in SpawnPoint)
            {
                var rngTier = Random.Range(1 , 156);
                if (rngTier <= 68)
                {
                    var rngitem = Random.Range(0, Common.Length);
                    Instantiate(Common[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 114)
                {
                    var rngitem = Random.Range(0, Common.Length);
                    Instantiate(Uncommon[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 138)
                {
                    var rngitem = Random.Range(0, Common.Length);
                    Instantiate(Rare[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 149)
                {
                    var rngitem = Random.Range(0, Common.Length);
                    Instantiate(Epic[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 155)
                {
                    var rngitem = Random.Range(0, Common.Length);
                    Instantiate(Cursed[rngitem], t.position ,Quaternion.identity);
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
