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
        public GameObject Ui;

        private void OnMouseEnter()
        {
            Animator.SetBool("select",true);
            Ui.SetActive(true);
        }

        private void OnMouseExit()
        {
            Animator.SetBool("select",false);
            Ui.SetActive(false);
        }
    }
}
