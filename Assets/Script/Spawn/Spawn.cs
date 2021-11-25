using System;
using Script.Controller;
using TMPro;
using UnityEngine;

namespace Script.Spawn
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject Enemy;
        [SerializeField] private Transform spawn;
        [SerializeField] private GameObject popup;
        
        private bool isTrigger = false;
        private Collider2D collider2D;

        private void Update()
        {
            PlayerController.playerInput.PlayerAction.Buy.performed += context => SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (isTrigger == true)
            {
                Instantiate(Enemy, spawn.position, Quaternion.identity);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isTrigger = true;
                popup.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            isTrigger = false;
            popup.SetActive(false);
        }

        
    }
}
