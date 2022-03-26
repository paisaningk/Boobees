using System;
using System.Collections;
using Script.Base;
using Script.Controller;
using Script.Pickup;
using Script.Sound;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Script.Spawn
{
    [System.Serializable]
    public class Wave
    {
        public string WaveName;
        public int numberOfEnemy;
        public float spawnTime;
        public GameObject[] typeOfEnemy;

    }
    public class SpawnWave : MonoBehaviour
    {
        [SerializeField] private Wave[] WaveForPlayerMelee;
        [SerializeField] private Wave[] WaveForPlayerGun;
        [SerializeField] private Transform[] SpawnPoint;
        [SerializeField] private TextMeshProUGUI WaveText;
        [SerializeField] private GameObject boss;
        [SerializeField] private GameObject Shop;
        [SerializeField] private GameObject win;
        [SerializeField] private float shopingtime = 20;
        [SerializeField] private TextMeshProUGUI Nextwavetext;
        [SerializeField] private GameObject nextwaveGameObject;
        [SerializeField] private GameObject PlayerSword;
        [SerializeField] private GameObject PlayerGun;
        [SerializeField] private Camera CameraMap;
        [SerializeField] private Camera CameraShop;
        [SerializeField] private Transform MapPoint;
        [SerializeField] private Transform ShopPoint;

        private Wave CurrentWave;
        private Wave[] Wave;
        private int CurrentWaveNumber = 0;
        private bool Counttimenextwave = false;
        private bool nextwave = true;
        private bool CanSpawn = true;
        private bool Soundplay = true;
        private float NextSpawnTime;
        public static int WaveNumberText = 1;
        private float timeshopshow;
        private ShopController ShopController;
        private bool canSpawn = false;
        [SerializeField] private GameObject Player;
        private PlayerCharacter playerCharacter;

        private void Awake()
        {
            if (SpawnPlayer.instance.PlayerType == PlayerType.Gun) 
            {
                Wave = WaveForPlayerGun;
                PlayerGun.SetActive(true);
            }
            else
            {
                Wave = WaveForPlayerMelee;
                PlayerSword.SetActive(true);
            }
            
        }
        private void Start()
        {
            WaveNumberText = 1;
            WaveText.text = $"Wave {WaveNumberText}";
            Shop.SetActive(false);
            timeshopshow = shopingtime;
            StartCoroutine(Wait());
            Player = GameObject.FindWithTag("Player");
            playerCharacter = Player.GetComponent<PlayerCharacter>();
        }

        private void FixedUpdate()
        {
            if (canSpawn)
            {
                if (CurrentWaveNumber <= Wave.Length)
                {
                    CurrentWave = Wave[CurrentWaveNumber];
                    spawnWave();
                    var tolalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                    WaveText.text = $"Wave : {WaveNumberText}";
                    if (tolalEnemies.Length == 0 && !CanSpawn && CurrentWaveNumber + 1 >= Wave.Length)
                    {
                        win.SetActive(true);
                        PlayerController.playerInput.PlayerAction.Disable();
                    }
                    else if (tolalEnemies.Length == 0 && !CanSpawn && CurrentWaveNumber + 1 != Wave.Length)
                    {
                        if (nextwave)
                        {
                            StartCoroutine(Shoping());
                            var coin = GameObject.FindGameObjectsWithTag("Coin");
                            Debug.Log(coin.Length);
                            foreach (var gold in coin)
                            {
                                playerCharacter.Gold += gold.GetComponent<Gold>().goldAmount;;
                                Destroy(gold);
                            }
                            Player.transform.position = ShopPoint.position;
                            nextwave = false;
                            timeshopshow = shopingtime;
                        }
                    }
                    else if (tolalEnemies.Length == 0 && !CanSpawn)
                    {
                        CurrentWaveNumber++;
                    }
                }
                if (Counttimenextwave)
                {
                    if (Soundplay)
                    {
                        SoundManager.Instance.Stop(SoundManager.Sound.BGM);
                        SoundManager.Instance.Play(SoundManager.Sound.Shop);
                        Soundplay = false;
                    }
                    nextwaveGameObject.SetActive(true);
                    var a = timeshopshow -= Time.deltaTime;
                    Nextwavetext.text = $"Next Wave in coming in {a:0.##} Sec";
                }
            }
        }
        
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(1.5f);
            canSpawn = true;
        }

        private void spawnWave()
        {
            if (CanSpawn && NextSpawnTime < Time.time)
            {
                SoundManager.Instance.Play(SoundManager.Sound.SpawnEnemy);
                var RandomEnemy = CurrentWave.typeOfEnemy[Random.Range(0, CurrentWave.typeOfEnemy.Length)];
                var RandomSpawnPoint = SpawnPoint[Random.Range(0, SpawnPoint.Length)];
                Instantiate(RandomEnemy, RandomSpawnPoint.position, Quaternion.identity);
                CurrentWave.numberOfEnemy--;
                NextSpawnTime = Time.time + CurrentWave.spawnTime;   
                Debug.Log($"numberOfEnemy {CurrentWave.numberOfEnemy}");
                if (CurrentWave.numberOfEnemy == 0)
                {
                    CanSpawn = false;
                }
            }
        }
        
        private IEnumerator Shoping()
        {
            CameraMap.gameObject.SetActive(false);
            CameraShop.gameObject.SetActive(true);
            SoundManager.Instance.Play(SoundManager.Sound.OpenShop);
            ShopController = Shop.GetComponent<ShopController>();
            Shop.SetActive(true);
            ShopController.RngItemandSpawn();
            Counttimenextwave = true;
            //shopOpen = true;
            
            yield return new WaitForSeconds(shopingtime);
            
            CameraMap.gameObject.SetActive(true);
            CameraShop.gameObject.SetActive(false);
            Shop.SetActive(false);
            ShopController.Deleteitem();
            NextSpawnWave();
            Counttimenextwave = false;
            nextwaveGameObject.SetActive(false);
            //shopOpen = false;

        }

        private void NextSpawnWave()
        {
            Player.transform.position = MapPoint.position;
            WaveNumberText++;
            CurrentWaveNumber++;
            CanSpawn = true;
            nextwave = true;
            Soundplay = true;
            if (CurrentWaveNumber == Wave.Length)
            {
                Instantiate(boss, transform.position, Quaternion.identity);
            }
            SoundManager.Instance.Stop(SoundManager.Sound.Shop);
            SoundManager.Instance.Playfrompause(SoundManager.Sound.BGM);
        }
    }
}
