using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject myPrefab;
    private int wave;
    private int EnemyInWave = 2;
    private int Enemyspeawn = 0;
    private float nextSpawnTime;
    private float spawnDelay = 1;

    private void Update()
    {
        SpawnedEnemy();
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
        
        
    }
    
    private void Spawnss()
    {
        nextSpawnTime = Time.time + spawnDelay;
        Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Enemyspeawn++;
        Debug.Log(Enemyspeawn);
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
