using System.Collections;
using Script.Controller;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


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
        [SerializeField] private GameObject Shop;
        [SerializeField] private Transform shopSpawnPoint;
        [SerializeField] private GameObject win;
        [SerializeField] private float shopingtime = 20;
        
        
        private Wave CurrentWave;
        private int CurrentWaveNumber = 0;
        private bool nextwave = true;
        private bool CanSpawn = true;
        private float NextSpawnTime;
        private int WaveNumberText =1;
        private ShopController ShopController;

        private void Start()
        {
            WaveText.text = $"Wave {WaveNumberText}";
        }

        private void Update()
        {
            if (CurrentWaveNumber < Wave.Length )
            {
                CurrentWave = Wave[CurrentWaveNumber];
                spawnWave();
                var tolalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                WaveText.text = $"Wave : {WaveNumberText}";
                if (tolalEnemies.Length == 0 && !CanSpawn && CurrentWaveNumber + 1 != Wave.Length)
                {
                    if (nextwave)
                    {
                        StartCoroutine(Shoping());
                        nextwave = false;
                    }
                    
                }
            }
            else
            {
                win.SetActive(true);
            }
        }

        private void spawnWave()
        {
            if (CanSpawn && NextSpawnTime < Time.time)
            {
                GameObject RandomEnemy = CurrentWave.typeOfEnemy[Random.Range(0, CurrentWave.typeOfEnemy.Length)];
                Transform RandomSpawnPoint = SpawnPoint[Random.Range(0, SpawnPoint.Length)];
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
            Debug.Log("shop open");
            
            yield return new WaitForSeconds(shopingtime);
            
            Shop.SetActive(false);
            ShopController.Deleteitem();
            NextSpawnWave();
            Debug.Log("shop close");
            
        }

        private void NextSpawnWave()
        {
            WaveNumberText++;
            CurrentWaveNumber++;
            CanSpawn = true;
            nextwave = true;
        }
        

        
    }
}
