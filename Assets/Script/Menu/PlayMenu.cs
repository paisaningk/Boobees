using System.Collections;
using System.Collections.Generic;
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
        [Header("Script")]
        [SerializeField] private AdsManager adsManager;
        [SerializeField] private ShopController shopController;
        [Header("UI")]
        [SerializeField] private GameObject pauseUi;
        [SerializeField] private GameObject deadUI;
        [SerializeField] private GameObject waveUI;
        //[SerializeField] private GameObject ScoreBoard;
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

        private int count = 0;
        public bool isPause = false;
        private float DashCd = 0;
        private bool candash = true;

        private void Awake()
        {
            resumeButton.onClick.AddListener(Resume);
            quitButton.onClick.AddListener(Quit);
            deadquitButton.onClick.AddListener(Quit);
            deadwinButton.onClick.AddListener(Quit);
            restartButton.onClick.AddListener(Restart);
            restartwinButton.onClick.AddListener(Restart);
            restartpauseButton.onClick.AddListener(Restart);
            Dash.fillAmount = 1;
        }

        private void Update()
        {
            goldText.text = $"Gold : {player.Gold}";
            var playerHp = player.Hp / player.MaxHp;
            blood.fillAmount = playerHp;

            if (PlayerController.CanDash == false)
            {
                if (candash == true)
                {
                    Dash.fillAmount = 0;
                    DashCd = 0;
                    Debug.Log("work candash");
                    candash = false;
                }
                DashCd += Time.deltaTime;
                Dash.fillAmount = DashCd / player.DashCd;
                Debug.Log(DashCd / player.DashCd);
                if (DashCd / player.DashCd >= 1)
                {
                    StartCoroutine(SetDashCd());
                }
            }
        }

        IEnumerator SetDashCd()
        {
            yield return new WaitForSeconds(0.1f);
            candash = true;
            StopCoroutine(SetDashCd());
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
            //ScoreBoard.SetActive(true);
        }

        
    }
}
