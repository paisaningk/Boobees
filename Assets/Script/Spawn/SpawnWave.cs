using System.Collections;
using System.Globalization;
using Script.Controller;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Sound;


namespace Script.Spawn
{
    [System.Serializable]
    public class Wave
    {
        public int numberOfEnemy;
        public float spawnTime;
        public GameObject[] typeOfEnemy;

    }
    public class SpawnWave : MonoBehaviour
    {
        [SerializeField] private Wave[] Wave;
        [SerializeField] private Transform[] SpawnPoint;
        [SerializeField] private TextMeshProUGUI WaveText;
        [SerializeField] private TextMeshProUGUI PlayerScore;
        [SerializeField] private GameObject Shop;
        [SerializeField] private GameObject win;
        [SerializeField] private GameObject Scoreboard;
        [SerializeField] private float shopingtime = 20;
        [SerializeField] private TextMeshProUGUI Nextwavetext;
        [SerializeField] private GameObject nextwaveGameObject;


        private Wave CurrentWave;
        private int CurrentWaveNumber = 0;
        private bool Counttimenextwave = false;
        private bool nextwave = true;
        private bool CanSpawn = true;
        private bool Soundplay = true;
        private float NextSpawnTime;
        private int WaveNumberText =1;
        private float timeshopshow;
        private ShopController ShopController;

        private void Start()
        {
            WaveText.text = $"Wave {WaveNumberText}";
            PlayerScore.text = $"{WaveNumberText}";
            timeshopshow = shopingtime;
        }

        private void Update()
        {

            if (CurrentWaveNumber < Wave.Length)
            {
                CurrentWave = Wave[CurrentWaveNumber];
                spawnWave();
                var tolalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                WaveText.text = $"Wave : {WaveNumberText}";
                PlayerScore.text = $"{WaveNumberText}";
                if (tolalEnemies.Length == 0 && !CanSpawn && CurrentWaveNumber + 1 != Wave.Length)
                {
                    if (nextwave)
                    {
                        StartCoroutine(Shoping());
                        nextwave = false;
                        timeshopshow = shopingtime;
                    }
                }
                else if (tolalEnemies.Length == 0 && !CanSpawn)
                {
                    CurrentWaveNumber++;
                }
            }
            else
            {
                win.SetActive(true);
                Scoreboard.SetActive(true);
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
                Nextwavetext.text = $"Next Wave in coming in {a:0.##} Sce";
            }
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

        IEnumerator Shoping()
        {
            ShopController = Shop.GetComponent<ShopController>();
            Shop.SetActive(true);
            ShopController.RngItemandSpawn();
            Counttimenextwave = true;
            
            yield return new WaitForSeconds(shopingtime);
            
            Shop.SetActive(false);
            ShopController.Deleteitem();
            NextSpawnWave();
            Counttimenextwave = false;
            nextwaveGameObject.SetActive(false);
            
        }

        private void NextSpawnWave()
        {
            WaveNumberText++;
            CurrentWaveNumber++;
            CanSpawn = true;
            nextwave = true;
            Soundplay = true;
            //SoundManager.Instance.Stop(SoundManager.Sound.Shop);
            SoundManager.Instance.Playfrompause(SoundManager.Sound.BGM);
        }
    }
}
