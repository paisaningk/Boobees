using UnityEngine;

namespace Assets.Script.Spawn
{
    public class SpawnItem : MonoBehaviour
    {
        [SerializeField]private Transform Spawn;
        [SerializeField]private GameObject Item;

        public void Start()
        {
            Instantiate(Item, Spawn.position ,Quaternion.identity);
        }
    }
}
