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
        [SerializeField] private String gameScene;
    
        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
            tutorialButton.onClick.AddListener(Tutorial);
            Time.timeScale = 1;
        }

        private void Tutorial()
        {
            SceneManager.LoadScene("Tutorial");
        }

        private void StartGame()
        {
            SceneManager.LoadScene(gameScene);
        }

        private void QuitGame()
        {
            Application.Quit();
            Debug.Log("it work");
        }
    }
}
