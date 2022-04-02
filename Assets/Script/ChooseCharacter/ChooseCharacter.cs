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
    
    [System.Serializable]
    public class SpawnPoint
    {
        public GameObject Ronin;
        public GameObject MarksMan;
    }

    public class ChooseCharacter : MonoBehaviour
    {
        public Camera Camera;
        public CinemachineVirtualCamera VirtualCamera;
        public LayerMask LayerMask;
        public GameObject PlayerRonin;
        public GameObject MarksMan;
        public static bool IsSelect = false;
        public GameObject Text;
        public SpawnPoint SpawnPoint;
        private Ray ray;
        private bool Ronin;
        private bool Mark;
        

        private void Awake()
        {
            Time.timeScale = 1;
            IsSelect = false;
        }
        void Update()
        {
            Mark = ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("MarksMan")).value;
            Ronin = ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("Ronin")).value;
            CheckAndChangeCharacter();
            ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (IsSelect) return;
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            if (Physics.Raycast(ray,out var hit,Mathf.Infinity,LayerMask))
            {
                var playAnimation = hit.transform.GetComponent<PlayAnimation>();
                    
                playAnimation.Ui.SetActive(false);
                hit.transform.gameObject.SetActive(false);
                Text.SetActive(false);
                IsSelect = true;

                if (playAnimation.PlayerType == PlayerType.SwordMan)
                {
                    ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("Ronin")).value = true;
                }
                else
                {
                    Camera.transform.position = MarksMan.transform.position;
                    VirtualCamera.Follow = MarksMan.transform;
                    ((Ink.Runtime.BoolValue) DialogueManager.GetInstance().GetVariableState("MarksMan")).value = true;
                }
            }
        }

        private void CheckAndChangeCharacter()
        {
            if (Mark)
            {
                Camera.transform.position = MarksMan.transform.position;
                VirtualCamera.Follow = MarksMan.transform;
                
                SpawnPoint.Ronin.SetActive(true);
                PlayerRonin.SetActive(false);
                PlayerRonin.transform.position = SpawnPoint.Ronin.transform.position;

                MarksMan.SetActive(true);
                SpawnPoint.MarksMan.SetActive(false);
                SpawnPlayer.instance.PlayerType = PlayerType.Gun;
            }
            else if (Ronin)
            {
                Camera.transform.position = PlayerRonin.transform.position;
                VirtualCamera.Follow = PlayerRonin.transform;
                
                SpawnPoint.MarksMan.SetActive(true);
                MarksMan.SetActive(false);
                MarksMan.transform.position = SpawnPoint.MarksMan.transform.position;

                PlayerRonin.SetActive(true);
                SpawnPoint.Ronin.SetActive(false);
                SpawnPlayer.instance.PlayerType = PlayerType.SwordMan;
            }
        }
    }
}