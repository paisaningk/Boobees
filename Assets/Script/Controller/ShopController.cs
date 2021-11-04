using System.Collections;
using Assets.scriptableobject.Item;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Transform  SpawnPoint01;
        [SerializeField] private Transform  SpawnPoint02;
        [SerializeField] private Transform  SpawnPoint03;
        [SerializeField] private GameObject[] Iteam;
        private Tier tier;
        
        
        public void Start()
        {
            StartCoroutine(Test());
            
            // Instantiate(Iteam[0], SpawnPoint01.position ,Quaternion.identity);
            // Instantiate(Iteam[0], SpawnPoint02.position ,Quaternion.identity);
            // Instantiate(Iteam[0], SpawnPoint03.position ,Quaternion.identity);
        }

        public void RngItemandSpawn()
        {
            var rngTier = Random.Range(1 , 156);
            if (rngTier < 30)
            {
                
            }
            else if (rngTier < 60)
            {
                
            }
            else if (rngTier < 70)
            {
                
            }
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(3);
            var rngTier = Random.Range(1 , 101);
            if (rngTier < 30)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 30");
            }
            else if (rngTier < 60)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 60");
            }
            else if (rngTier < 70)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 70");
            }
            else
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 100");
            }
            StartCoroutine(Test());
        }
    }
}
