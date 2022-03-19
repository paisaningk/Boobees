using Script.Controller;
using Script.Spawn;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    //this Script use in Choose Character Scene
    //It is responsible for Choose Character and popup confirm 
    public enum SceneName
    {
        Map1,
        Tutorial,
    }

    public class ChooseCharacter : MonoBehaviour
    {
        public Camera Camera;
        public SceneName Scene;
        public PlayerType PlayerType;
        public SpawnPlayer SpawnPlayer;
        public LayerMask LayerMask;
        public GameObject selectUI;
        public TextMeshProUGUI text;
        public Button yes;
        public Button no;
        public Button Main;
        private GameObject Player;


        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("SpawnPlayer");
            SpawnPlayer = Player.GetComponent<SpawnPlayer>();
            yes.onClick.AddListener(Select);
            no.onClick.AddListener(NO);
            Main.onClick.AddListener(MainMenu);
        }

        void Update()
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit ;
            if (Physics.Raycast(ray, out hit,1000,LayerMask))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hit.transform.GetComponent<PlayAnimation>().Lock == false)
                    {
                        selectUI.SetActive(true);
                        PlayerType = hit.transform.GetComponent<PlayAnimation>().PlayerType;
                        if (PlayerType == PlayerType.Gun)
                        {
                            text.text = $"Your have selected Marksman";
                        }
                        else
                        {
                            text.text = $"Your have selected Ronin";
                        }
                    }
                }
            }
        }

        void Select()
        {
            SpawnPlayer.PlayerType = PlayerType;
            DontDestroyOnLoad(SpawnPlayer);
            SceneManager.LoadScene($"{Scene}");
        }

        void NO()
        {
            selectUI.SetActive(false);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu_PC");
        }
        
    }
}