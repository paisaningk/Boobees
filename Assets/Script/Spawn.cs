using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject myPrefab;
    [SerializeField] private TextMeshProUGUI waveText;
    private int wave = 1;
    private int EnemyInWave = 2;
    private int Enemyspeawn = 0;
    private float nextSpawnTime;
    private float spawnDelay = 1;

    private void Start()
    {
        SetTextWave();
    }

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
        Debug.Log($"Spwan Enemy {Enemyspeawn} คน");
    }

    private bool waveend()
    {
        return Enemyspeawn == EnemyInWave;
    }
    
    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }

    private void SetTextWave()
    {
        waveText.text = $"Wave : {wave}";
    }
}
