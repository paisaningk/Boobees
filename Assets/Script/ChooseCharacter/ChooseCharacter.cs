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
        public CinemachineVirtualCamera VirtualCamera;
        public LayerMask LayerMask;
        public GameObject PlayerRonin;
        public GameObject PlayerGun;
        public bool IsSelect = false;
        public GameObject Text;
        private Ray ray;
        

        void Update()
        {
            ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (IsSelect) return;
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            if (Physics.Raycast(ray,out var hit,Mathf.Infinity,LayerMask))
            {
                var playAnimation = hit.transform.GetComponent<PlayAnimation>();
                    
                playAnimation.Ui.SetActive(false);
                hit.transform.gameObject.SetActive(false);
                Text.SetActive(false);
                SpawnPlayer.instance.PlayerType = playAnimation.PlayerType;
                IsSelect = true;

                if (playAnimation.PlayerType == PlayerType.SwordMan)
                {
                    PlayerRonin.SetActive(true);
                    Camera.transform.position = PlayerRonin.transform.position;
                    VirtualCamera.Follow = PlayerRonin.transform;
                }
                else
                {
                    PlayerGun.SetActive(true);
                    Camera.transform.position = PlayerGun.transform.position;
                    VirtualCamera.Follow = PlayerGun.transform;
                }
            }
        }
    }
}