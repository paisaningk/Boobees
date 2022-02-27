using System;
using Script.Controller;
using Script.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class PlayAnimation : MonoBehaviour
    {
        public Animator Animator;
        public PlayerType PlayerType;
        public SpriteRenderer SpriteRenderer;
        public GameObject Text;
        public GameObject Text01;
        public GameObject Save;
        private int Killneed = 20;
        public bool Lock = false;

        private void Start()
        {
            DontDestroyOnLoad(Save);
        }


        public void Update()
        {
            if (SaveData.Instance.Wave == false && PlayerType == PlayerType.Gun)
            {
                SpriteRenderer.color = Color.black;
                Text.SetActive(false);
                Text01.SetActive(true);
                Lock = true;
            }
            else
            {
                SpriteRenderer.color = Color.white;
                Text01.SetActive(false);
                Text.SetActive(true);
                Lock = false;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                SaveData.Instance.Wave = true;
                SaveData.Instance.Killednow = 20;
                Lock = true;
            }
        }

        private void OnMouseEnter()
        {
            if (SaveData.Instance.Wave == true)
            {
                Animator.SetBool("select",true);
            }
        }

        private void OnMouseExit()
        {
            if (SaveData.Instance.Wave == true)
            {
                Animator.SetBool("select",false);
            }
        }
    }
}
