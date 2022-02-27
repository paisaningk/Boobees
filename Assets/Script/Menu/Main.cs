using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Menu
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button tutorialButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Texture2D cursorTexture;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
            tutorialButton.onClick.AddListener(Tutorial);
            Time.timeScale = 1;
        }

        private void Tutorial()
        {
            SceneManager.LoadScene("ChooseCharacterTutorial");
        }

        private void StartGame()
        {
            SceneManager.LoadScene("ChooseCharacter");
        }

        private void QuitGame()
        {
            Application.Quit();
            Debug.Log("it work");
        }
    }
}
