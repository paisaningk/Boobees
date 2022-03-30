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
        public ChooseCharacter ChooseCharacter;
        public Animator Animator;
        public PlayerType PlayerType;
        public GameObject Ui;
        [Header("Text")]
        public TextAsset InkJson;
        public Sprite ImageProfile;
        public string Name;
        private bool playerInRange = false;

        private void Update()
        {
            //Button.SetActive(playerInRange);
            if (playerInRange)
            {
                if (Input.GetKeyDown(KeyCode.E) && DialogueManager.GetInstance().DialoguePlaying == false)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(InkJson,Name,ImageProfile);
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        private void OnMouseEnter()
        {
            if (!ChooseCharacter.IsSelect)
            {
                Animator.SetBool("select",true);
                Ui.SetActive(true);
            }
        }

        private void OnMouseExit()
        {
            if (!ChooseCharacter.IsSelect)
            {
                Animator.SetBool("select",false);
                Ui.SetActive(false);    
            }
            
        }
    }
}
