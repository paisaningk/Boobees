using System.Collections;
using Assets.Script.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Controller
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Transform[]  spawnPoint;
        [SerializeField] private GameObject[] common;
        [SerializeField] private GameObject[] uncommon;
        [SerializeField] private GameObject[] rare;
        [SerializeField] private GameObject[] epic;
        [SerializeField] private GameObject[] cursed;
        [SerializeField] private GameObject shopMenu;
        [SerializeField] private GameObject ePopup;
        [SerializeField] private Button backButton;
        [SerializeField] private Button rngButton;
        [SerializeField] private Button healButton;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI healCostText;
        [SerializeField] private TextMeshProUGUI rngText;
        
        private int healCost = 20;
        private int rngCost = 20;
        private bool _shop = false;
        private Playerinput _playerInput;
        private Collider2D _player;
        
        
        public void Start()
        { 
            //RngItemandSpawn();
            _playerInput = new Playerinput();
            _playerInput.PlayerAction.Buy.performed += context => Talk();
            
            rngButton.onClick.AddListener(CheckGoldForReroll);
            backButton.onClick.AddListener(Back);
            healButton.onClick.AddListener(Heal);
            
            
            _playerInput.Enable();
        }
        
        

        private void Talk()
        {
            if (_shop == true)
            {
                string[] talk = new[] {"Hello , You cum again ","Hello nigga" , "you give me a monkey" };
                string[] talka = new[] {"Hello","Hello adc" , "you give me" };
                var range = Random.Range(0, talk.Length);
                shopMenu.SetActive(true);
                text.text = talka[range];
                healCostText.text = $"{healCost}";
                rngText.text = $"{rngCost}";
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
                _player = other;
                _shop = true;
                ePopup.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _shop = false;
            ePopup.SetActive(false);
        }

        public void CheckGoldForReroll()
        {
            var playerCharacter = _player.GetComponent<PlayerCharacter>();
            if (playerCharacter.Gold >= rngCost)
            {
                playerCharacter.Gold -= rngCost;
                Deleteitem();
                RngItemandSpawn();
            }
            else
            {
                text.text = $"GOLD not enough";
            }
        }

        public void Deleteitem()
        {
            var iteminscene = GameObject.FindGameObjectsWithTag("Item");
            foreach (var item in iteminscene)
            {
                Destroy(item);
            }
        }

        private void Heal()
        {
            var playerCharacter = _player.GetComponent<PlayerCharacter>();
            if (playerCharacter.Gold >= healCost)
            {
                playerCharacter.Gold -= healCost;
                //var heal50= playerCharacter.MaxHp / 2;
                playerCharacter.Hp += healCost;
                if (playerCharacter.Hp >= playerCharacter.MaxHp)
                {
                    playerCharacter.Hp = playerCharacter.MaxHp;
                }
            }
            else

            {
                text.text = $"GOLD not enough";
            }
        }

       public void RngItemandSpawn()
        {
            foreach (var t in spawnPoint)
            {
                var rngTier = Random.Range(1 , 156);
                if (rngTier <= 68)
                {
                    var rngitem = Random.Range(0, common.Length);
                    Instantiate(common[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 114)
                {
                    var rngitem = Random.Range(0, common.Length);
                    Instantiate(uncommon[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 138)
                {
                    var rngitem = Random.Range(0, common.Length);
                    Instantiate(rare[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 149)
                {
                    var rngitem = Random.Range(0, common.Length);
                    Instantiate(epic[rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 155)
                {
                    var rngitem = Random.Range(0, common.Length);
                    Instantiate(cursed[rngitem], t.position ,Quaternion.identity);
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
