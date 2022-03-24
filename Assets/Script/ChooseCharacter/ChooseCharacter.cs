using System;
using Cinemachine;
using Script.Controller;
using Script.Spawn;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    //this Script use in Choose Character Scene
    //It is responsible for Choose Character and popup confirm 

    public class ChooseCharacter : MonoBehaviour
    {
        public Camera Camera;
        public new CinemachineVirtualCamera camera;
        public LayerMask LayerMask;
        public GameObject PlayerRonin;
        public GameObject PlayerGun;
        public GameObject Text;
        private Ray ray;
        

        void Update()
        {
            ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            if (Physics.Raycast(ray,out var hit,Mathf.Infinity,LayerMask))
            {
                var playAnimation = hit.transform.GetComponent<PlayAnimation>();
                    
                playAnimation.Ui.SetActive(false);
                hit.transform.gameObject.SetActive(false);
                Text.SetActive(false);
                SpawnPlayer.instance.PlayerType = playAnimation.PlayerType;
                    
                if (playAnimation.PlayerType == PlayerType.SwordMan)
                {
                    PlayerRonin.SetActive(true);
                    camera.Follow = PlayerRonin.transform;
                }
                else
                {
                    PlayerGun.SetActive(true);
                    camera.Follow = PlayerGun.transform;
                }
            }
        }
    }
}