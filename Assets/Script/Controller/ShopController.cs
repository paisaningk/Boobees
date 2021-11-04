using System.Collections;
using Assets.scriptableobject.Item;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Transform[]  SpawnPoint;
        [SerializeField] private GameObject[] Common;
        [SerializeField] private GameObject[] Uncommon;
        [SerializeField] private GameObject[] Rare;
        [SerializeField] private GameObject[] Epic;
        [SerializeField] private GameObject[] Cursed;
        
        
        public void Start()
        {
            //StartCoroutine(Test());
            
            RngItemandSpawn();
            
        }

        public void RngItemandSpawn()
        {
            foreach (var t in SpawnPoint)
            {
                var rngTier = Random.Range(1 , 156);
                if (rngTier <= 68)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Common[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 114)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Uncommon[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 138)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Rare[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 149)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Epic[Rngitem], t.position ,Quaternion.identity);
                }
                else if (rngTier <= 155)
                {
                    var Rngitem = Random.Range(0, Common.Length);
                    Instantiate(Cursed[Rngitem], t.position ,Quaternion.identity);
                }
            }
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(3);
            var rngTier = Random.Range(1 , 156);
            if (rngTier <= 68)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 68");
            }
            else if (rngTier <= 114)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 114");
            }
            else if (rngTier <= 138)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 138");
            }
            else if (rngTier <= 149)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 149");
            }
            else if (rngTier <= 155)
            {
                Debug.Log($"rngTier = {rngTier}");
                Debug.Log("rngTier < 155");
            }
            StartCoroutine(Test());
        }
    }
}
