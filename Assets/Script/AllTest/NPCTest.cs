using System;
using UnityEngine;

public class NpcTest : MonoBehaviour
{
    [Header("Button")]
    public GameObject Button;
    [Header("Text")]
    public TextAsset InkJson;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().EnterDialogueMode(InkJson);
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
            DialogueManager.GetInstance().ExitDialogueMode();
        }
    }
}
