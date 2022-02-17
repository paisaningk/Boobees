using System.Collections;
using Script.Base;
using Script.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Menu
{
    public class PlayMenu : MonoBehaviour
    {
        [Header("Script")]
        [SerializeField] private ShopController shopController;
        [Header("UI")]
        [SerializeField] private GameObject pauseUi;
        [SerializeField] private GameObject deadUI;
        [SerializeField] private GameObject waveUI;
        [SerializeField] private GameObject StatusUI;
        //[SerializeField] private GameObject ScoreBoard;
        //[SerializeField] private GameObject phoneUI;
        [Header("Button")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button deadquitButton;
        [SerializeField] private Button deadwinButton;
        [SerializeField] private Button restartwinButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button restartpauseButton;
        [Header("Player")]
        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private PlayerCharacter player;
        [SerializeField] private Image blood;
        [SerializeField] private Image Dash;
        [Header("Status")]
        [SerializeField] private TextMeshProUGUI MaxHPText;
        [SerializeField] private TextMeshProUGUI AtkText;
        [SerializeField] private TextMeshProUGUI SpeedText;
        [SerializeField] private TextMeshProUGUI DashCdText;
        [SerializeField] private TextMeshProUGUI CritRateText;
        [SerializeField] private TextMeshProUGUI GoldText;
        [SerializeField] private Button quitStatusButton;
        
        public static bool Isphone;
        public bool isPause = false;
        private float DashCd = 0;
        private bool candash = true;
        private bool StatusShow = false;

        private void Awake()
        {
            resumeButton.onClick.AddListener(Resume);
            quitButton.onClick.AddListener(Quit);
            deadquitButton.onClick.AddListener(Quit);
            deadwinButton.onClick.AddListener(Quit);
            restartButton.onClick.AddListener(Restart);
            restartwinButton.onClick.AddListener(Restart);
            restartpauseButton.onClick.AddListener(Restart);
            quitStatusButton.onClick.AddListener(Back);
            Dash.fillAmount = 1;
        }

        private void Start()
        {
            PlayerController.playerInput.PlayerAction.Status.performed += context => OpenStatus();
        }

        private void Update()
        {
            goldText.text = $"Gold : {player.Gold}";
            var playerHp = player.Hp / player.MaxHp;
            blood.fillAmount = playerHp;
            SetStatus();
            if (PlayerController.CanDash == false)
            {
                if (candash == true)
                {
                    Dash.fillAmount = 0;
                    DashCd = 0;
                    candash = false;
                }
                DashCd += Time.deltaTime;
                Dash.fillAmount = DashCd / player.DashCd;
                //Debug.Log(DashCd / player.DashCd);
                if (DashCd / player.DashCd >= 1)
                {
                    StartCoroutine(SetDashCd());
                }
            }
        }
        
        public void OpenStatus()
        {
            if (StatusShow == false)
            {
                //phoneUI.SetActive(false);
                StatusUI.SetActive(true);
                StatusShow = true;
                Time.timeScale = 0;
            }
            else
            {
                Back();
            }
            
        }

        private void Back()
        {
            StatusUI.SetActive(false);
            StatusShow = false;
            Time.timeScale = 1;
            //phoneUI.SetActive(true);
        }

        IEnumerator SetDashCd()
        {
            yield return new WaitForSeconds(0.1f);
            candash = true;
            StopCoroutine(SetDashCd());
        }

        private void SetStatus()
        {
            MaxHPText.text = $"{player.MaxHp}";
            SpeedText.text = $"{player.Speed}";
            AtkText.text = $"{player.Atk}";
            DashCdText.text = $"{player.DashCd}";
            CritRateText.text = $"{player.CritRate}";
            GoldText.text = $"{player.Gold}";
        }
        
        public void Pause()
        {
            if (shopController.shoping == false)
            {
                //phoneUI.SetActive(false);
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
            //phoneUI.SetActive(true);
            pauseUi.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            PlayerController.playerInput.PlayerAction.Attack.Enable();
            PlayerController.playerInput.PlayerAction.Dash.Enable();
            PlayerController.playerInput.PlayerAction.Move.Enable();
        }

        private void Quit()
        {
            SceneManager.LoadScene("MainMenu_PC");
        }
        
        private void Restart()
        {
            SceneManager.LoadScene("Scenes/Kao");
        }

        public void Dead()
        {
            pauseUi.SetActive(false);
            waveUI.SetActive(false);
            deadUI.SetActive(true);
        }

        
    }
}
