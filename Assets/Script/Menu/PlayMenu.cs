using Assets.Script.Base;
using Script.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Script.Menu
{
    public class PlayMenu : MonoBehaviour
    {
        [SerializeField] private AdsManager adsManager;
        [SerializeField] private ShopController shopController;
        [SerializeField] private GameObject pauseUi;
        [SerializeField] private GameObject deadUI;
        [SerializeField] private GameObject waveUI;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button deadquitButton;
        [SerializeField] private Button deadwinButton;
        [SerializeField] private Button restartwinButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button restartpauseButton;
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private Image blood;
        
        private int count = 0;
        public bool isPause = false;

        private void Awake()
        {
            resumeButton.onClick.AddListener(Resume);
            quitButton.onClick.AddListener(Quit);
            deadquitButton.onClick.AddListener(Quit);
            deadwinButton.onClick.AddListener(Quit);
            restartButton.onClick.AddListener(Restart);
            restartwinButton.onClick.AddListener(Restart);
            restartpauseButton.onClick.AddListener(Restart);
        }

        private void Update()
        {
            goldText.text = $"Gold : {player.Gold}";
            var a = player.Hp / player.MaxHp;
            blood.fillAmount = a;
        }
        
        public void Pause()
        {
            if (shopController.shoping == false)
            {
                
                PlayerController.playerInput.PlayerAction.Attack.Disable();
                PlayerController.playerInput.PlayerAction.Dash.Disable();
                PlayerController.playerInput.PlayerAction.Move.Disable();
                pauseUi.SetActive(true);
                Time.timeScale = 0;
                isPause = true;
            }
        }
        public void Resume()
        {
            pauseUi.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            PlayerController.playerInput.PlayerAction.Attack.Enable();
            PlayerController.playerInput.PlayerAction.Dash.Enable();
            PlayerController.playerInput.PlayerAction.Move.Enable();
        }

        private void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        private void Restart()
        {
            // if (count <= 0)
            // {
            //     count++;
            //     adsManager.ShowAds("Rewarded_Android");
            // }
            // else
            // {
            //     SceneManager.LoadScene("MainMenu");
            // }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Dead()
        {
            pauseUi.SetActive(false);
            waveUI.SetActive(false);
            deadUI.SetActive(true);
        }
    }
}
