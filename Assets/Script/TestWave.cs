using TMPro;
using UnityEngine;


namespace Assets.Script
{
    [System.Serializable]
    public class Wave
    {
        public string WaveName;
        public int NumberOfEnemy;
        public GameObject[] TypeOfEnemy;
        public float SpawnTime;
    }
    public class TestWave : MonoBehaviour
    {
        [SerializeField] private Wave[] Wave;
        [SerializeField] private Transform[] SpawnPoint;
        [SerializeField] private TextMeshProUGUI WaveText;

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
            GameObject[] TolalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (TolalEnemies.Length == 0 && !CanSpawn && CurrentWaveNumber +1 != Wave.Length)
            {
                NextSpawnWave();
            }
        }

        private void SpawnWave()
        {
            if (CanSpawn && NextSpawnTime < Time.time)
            {
                GameObject RandomEnemy = CurrentWave.TypeOfEnemy[Random.Range(0, CurrentWave.TypeOfEnemy.Length)];
                Transform RandomSpawnPoint = SpawnPoint[Random.Range(0, SpawnPoint.Length)];
                Instantiate(RandomEnemy, RandomSpawnPoint.position, Quaternion.identity);
                
                CurrentWave.NumberOfEnemy--;
                NextSpawnTime = Time.time + CurrentWave.SpawnTime;   
                
                if (CurrentWave.NumberOfEnemy == 0)
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
            WaveText.text = $"Wave {CurrentWave}";
        }
    }
}
