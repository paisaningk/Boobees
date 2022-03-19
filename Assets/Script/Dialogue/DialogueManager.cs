using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
   public string WaveName;
   public int numberOfEnemy;
   public float spawnTime;
   public GameObject[] typeOfEnemy;

}

public class DialogueManager : MonoBehaviour
{
   [Header("Dialogue UI")]
   [SerializeField] private GameObject Dialogue;
   [SerializeField] private TextMeshProUGUI DialogueText;
   [SerializeField] private TextMeshProUGUI NameText;
   [SerializeField] private Image ImageProfile;
   
   private static DialogueManager instance;
   private Story currentStory;
   private bool dialoguePlaying;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         Debug.LogWarning("Found more than one Dialogue Manager in scene");
         Destroy(gameObject);
      }
      dialoguePlaying = false;
      Dialogue.SetActive(dialoguePlaying);
   }

   public static DialogueManager GetInstance()
   {
      return instance;
   }

   private void Update()
   {
      if (!dialoguePlaying)
      {
         return;
      }

      if (Input.GetKeyDown(KeyCode.E))
      {
         ContinueStory();
         
      }
   }

   private void ContinueStory()
   {
      if (currentStory.canContinue)
      {
         DialogueText.text = currentStory.Continue();
      }
      else
      {
         ExitDialogueMode();
      }
      Debug.Log(currentStory.canContinue);
   }

   public void EnterDialogueMode(TextAsset ink)
   {
      currentStory = new Story(ink.text);
      dialoguePlaying = true;
      Dialogue.SetActive(dialoguePlaying);

      ContinueStory();
   }

   public void ExitDialogueMode()
   {
      dialoguePlaying = false;
      Dialogue.SetActive(dialoguePlaying);
      DialogueText.text = null;
   }
}
