using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
   [Header("Dialogue UI")]
   [SerializeField] private GameObject Dialogue;
   [SerializeField] private TextMeshProUGUI DialogueText;
   [SerializeField] private TextMeshProUGUI NameText;
   [SerializeField] private Image ImageProfile;

   [Header("DialogueChoices")] 
   [SerializeField] private GameObject[] ChoiceGameObjects;
   private TextMeshProUGUI[] choiceTexts;
   
   private static DialogueManager instance;
   private Story currentStory;
   public bool DialoguePlaying;

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
      DialoguePlaying = false;
      Dialogue.SetActive(DialoguePlaying);

      choiceTexts = new TextMeshProUGUI[ChoiceGameObjects.Length];
      for (var i = 0; i < ChoiceGameObjects.Length; i++)
      { 
         choiceTexts[i] =ChoiceGameObjects[i].GetComponentInChildren<TextMeshProUGUI>();
      }
      
   }

   public static DialogueManager GetInstance()
   {
      return instance;
   }

   private void Update()
   {
      if (!DialoguePlaying)
      {
         return;
      }

      if (currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.E))
      {
         ContinueStory();
      }
   }

   private void ContinueStory()
   {
      if (currentStory.canContinue) 
      {
         // set text for the current dialogue line
         DialogueText.text = currentStory.Continue();
         // display choices, if any, for this dialogue line
         DisplayChoices();
      }
      else 
      {
         StartCoroutine(ExitDialogueMode());
      }
   }

   public void EnterDialogueMode(TextAsset ink,string name,Sprite imageProfile)
   {
      NameText.text = $"{name}";
      ImageProfile.sprite = imageProfile;
      StartCoroutine(EnterDialogueDelay(ink));
   }

   public IEnumerator EnterDialogueDelay(TextAsset ink)
   {
      yield return new WaitForSeconds(0.1f);
      currentStory = new Story(ink.text);
      DialoguePlaying = true;
      Dialogue.SetActive(DialoguePlaying);
      ContinueStory();
   }

   public IEnumerator ExitDialogueMode()
   {
      Dialogue.SetActive(false);
      yield return new WaitForSeconds(0.1f);
      DialoguePlaying = false;
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
         choiceTexts[index].text = choice.text;
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
      currentStory.ChooseChoiceIndex(choiceIndex);
      ContinueStory();
   }
}
