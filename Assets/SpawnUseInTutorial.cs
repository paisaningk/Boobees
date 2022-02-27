using System.Collections;
using System.Collections.Generic;
using Script.Controller;
using Script.Spawn;
using UnityEngine;

public class SpawnUseInTutorial : MonoBehaviour
{
    [SerializeField] private GameObject PlayerSword;
    [SerializeField] private GameObject PlayerGun;
    private GameObject Player;
    private SpawnPlayer spawnPlayer;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("SpawnPlayer");
        spawnPlayer = Player.GetComponent<SpawnPlayer>();
        if (spawnPlayer.PlayerType == PlayerType.Gun) 
        {
            
            PlayerGun.SetActive(true);
        }
        else
        {
            PlayerSword.SetActive(true);
        }
    }
}
