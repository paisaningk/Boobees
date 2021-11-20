using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Script.Menu
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private String gameScene;
    
        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
            Time.timeScale = 1;
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
