using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

   [Header("DialogueChoices")] 
   [SerializeField] private GameObject[] ChoiceGameObjects;
   private TextMeshProUGUI[] ChoiceTexts;
   
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
      
      //set dialogue set active false
      dialoguePlaying = false;
      Dialogue.SetActive(dialoguePlaying);

      ChoiceTexts = new TextMeshProUGUI[ChoiceGameObjects.Length];
      for (var i = 0; i < ChoiceGameObjects.Length; i++)
      { 
         ChoiceTexts[i] =ChoiceGameObjects[i].GetComponentInChildren<TextMeshProUGUI>();
      }
      
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
      Debug.Log(currentStory.currentChoices);
   }

   private void ContinueStory()
   {
      if (currentStory.canContinue)
      {
         //set text for the current dialogue line
         DialogueText.text = currentStory.Continue();
         
         // display choice, if any, for this dialogue line
         DisplayChoices();
        
      }
      else
      {
         ExitDialogueMode();
      }
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

   private void DisplayChoices()
   {
      List<Choice> currentChoices = currentStory.currentChoices;

      // defensive check to make sure our UI can support the number of choices coming in
      if (currentChoices.Count > ChoiceGameObjects.Length)
      {
         Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                        + currentChoices.Count);
      }

      int index = 0;
      // enable and initialize the choices up to the amount of choices for this line of dialogue
      foreach(Choice choice in currentChoices) 
      {
         ChoiceGameObjects[index].gameObject.SetActive(true);
         ChoiceTexts[index].text = choice.text;
         index++;
      }
      // go through the remaining choices the UI supports and make sure they're hidden
      for (int i = index; i < ChoiceGameObjects.Length; i++) 
      {
         ChoiceGameObjects[i].gameObject.SetActive(false);
      }

   }

   // private IEnumerator SelectFirstChoice()
   // {
   //    //Event System requires we clear it first, than wait
   //    //for at least one frame before we set the current selected object.
   //    EventSystem.current.SetSelectedGameObject(null);
   //    yield return new WaitForEndOfFrame();
   //    EventSystem.current.SetSelectedGameObject(ChoiceGameObjects[0].gameObject);
   // }

   public void MakeChoice(int choiceIndex)
   {
      Debug.Log(choiceIndex);
      currentStory.ChooseChoiceIndex(choiceIndex);
      ContinueStory();
   }
}
