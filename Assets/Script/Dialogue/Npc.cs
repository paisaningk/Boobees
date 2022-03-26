using System;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    [Header("Button")]
    public GameObject Button;
    [Header("Text")]
    public TextAsset InkJson;
    public Sprite ImageProfile;
    public string Name;
    private bool playerInRange;


    private void Start()
    {
        Button.SetActive(false);
        playerInRange = false;
    }

    private void Update()
    {
        Button.SetActive(playerInRange);
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
            StartCoroutine(DialogueManager.GetInstance().ExitDialogueMode());
        }
    }
}
