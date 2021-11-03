using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Menu
{
    public class PlayMenu : MonoBehaviour
    {
        [SerializeField] private AdsManager adsManager;

        [SerializeField] private GameObject pauseUi;
        [SerializeField] private GameObject DeadUI;
        [SerializeField] private GameObject WaveUI;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button deadquitButton;
        [SerializeField] private Button restartButton;
        public bool isPause = false;

        private void Awake()
        {
            resumeButton.onClick.AddListener(Resume);
            quitButton.onClick.AddListener(Quit);
            deadquitButton.onClick.AddListener(Quit);
            restartButton.onClick.AddListener(Restart);
        }
        public void Pause()
        {
            pauseUi.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
        }
        public void Resume()
        {
            pauseUi.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }

        private void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        private void Restart()
        {
            var count = 0;
            if (count <= 0)
            {
                count++;
                adsManager.ShowAds("Rewarded_Android");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        public void Dead()
        {
            pauseUi.SetActive(false);
            WaveUI.SetActive(false);
            DeadUI.SetActive(true);
        }
    }
}
