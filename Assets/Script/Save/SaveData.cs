using System;
using UnityEngine;

namespace Script.Save
{
    [System.Serializable]
    public class SaveData : MonoBehaviour
    {
        public bool Wave;
        public int Killednow;
        public bool a;
        
        public static SaveData Instance { get; private set; }
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /*public SaveData(SaveData saveData)
        {
            Wave = saveData.Wave;
            Killednow = saveData.Killednow;
        }

        private SaveData(bool wave,int killednow)
        {
            Wave = wave;
            Killednow = killednow;
        }

        public void SavePlayer()
        {
            SaveSysetm.SavePlayer(this);
        }

        public void LoadPlayer()
        {
            SaveData data = SaveSysetm.LoadPlayer();
            Wave = data.Wave;
            Killednow = data.Killednow;
        }*/

    }
}
