using Script.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Menu
{
    public class PlayMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUi;
        [SerializeField] private GameObject DeadUI;
        [SerializeField] private GameObject WaveUI;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(Resume);
            quitButton.onClick.AddListener(Quit);
        }
        public void Pause()
        {
            pauseUi.SetActive(true);
            Time.timeScale = 0;

        }
        public void Resume()
        {
            pauseUi.SetActive(false);
            Time.timeScale = 1;
        }

        private void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Dead()
        {
            pauseUi.SetActive(false);
            WaveUI.SetActive(false);
            DeadUI.SetActive(true);
        }
        
    }
}
