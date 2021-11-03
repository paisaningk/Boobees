using TMPro;
using UnityEngine;

namespace Assets.Script.Spawn
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject myPrefab;
        [SerializeField] private TextMeshProUGUI Wave;
        private int waveNumber = 1;
        private int EnemyInWave = 2;
        private int Enemyspeawn = 0;
        private float nextSpawnTime;
        private float spawnDelay = 1;

        private void Update()
        {
            SpawnedEnemy();
            Wave.text = $"Waveaaaa : {waveNumber}";
        }

        public void SpawnedEnemy()
        {
            if (waveend() != true)
            {
                if (ShouldSpawn())
                {
                    Spawnss();
                }
            }
            else
            {
                var Enemydog = GameObject.FindGameObjectsWithTag("Enemy");
            
                if (Enemydog == null)
                {
                    EnemyInWave += 10;
                    waveNumber++;
                    print("next wave");
                }
            }
        }
    
        private void Spawnss()
        {
            nextSpawnTime = Time.time + spawnDelay;
            Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Enemyspeawn++;
            //Debug.Log(Enemyspeawn);
        }

        private bool waveend()
        {
            return Enemyspeawn == EnemyInWave;
        }
    
        private bool ShouldSpawn()
        {
            return Time.time >= nextSpawnTime;
        }
    }
}
