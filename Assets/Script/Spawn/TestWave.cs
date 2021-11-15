using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Script.Spawn
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int numberOfEnemy;
        public float spawnTime;
        public GameObject[] typeOfEnemy;

    }
    public class TestWave : MonoBehaviour
    {
        [SerializeField] private Wave[] Wave;
        [SerializeField] private Transform[] SpawnPoint;
        [SerializeField] private TextMeshProUGUI WaveText;
        [SerializeField] private GameObject Shop;
        
        
        private Wave CurrentWave;
        private int CurrentWaveNumber;
        private bool CanSpawn = true;
        private float NextSpawnTime;
        private int WaveNumberText =1;

        private void Start()
        {
            WaveText.text = $"Wave {WaveNumberText}";
        }

        private void Update()
        {
            CurrentWave = Wave[CurrentWaveNumber];
            SpawnWave();
            var tolalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            WaveText.text = $"Wave : {WaveNumberText}";
            if (tolalEnemies.Length == 0 && !CanSpawn && CurrentWaveNumber +1 != Wave.Length)
            {
                NextSpawnWave();
            }
        }

        private void SpawnWave()
        {
            if (CanSpawn && NextSpawnTime < Time.time)
            {
                GameObject RandomEnemy = CurrentWave.typeOfEnemy[Random.Range(0, CurrentWave.typeOfEnemy.Length)];
                Transform RandomSpawnPoint = SpawnPoint[Random.Range(0, SpawnPoint.Length)];
                Instantiate(RandomEnemy, RandomSpawnPoint.position, Quaternion.identity);
                
                CurrentWave.numberOfEnemy--;
                NextSpawnTime = Time.time + CurrentWave.spawnTime;   
                
                if (CurrentWave.numberOfEnemy == 0)
                {
                    CanSpawn = false;
                }
            }
        }

        private void NextSpawnWave()
        {
            WaveNumberText++;
            CurrentWaveNumber++;
            CanSpawn = true;
            
        }
        
    }
}
