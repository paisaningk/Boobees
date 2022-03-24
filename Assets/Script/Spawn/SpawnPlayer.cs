using Script.Controller;
using UnityEngine;

namespace Script.Spawn
{
    public class SpawnPlayer : MonoBehaviour
    {
        public static SpawnPlayer instance;
        public PlayerType PlayerType;
        void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}
