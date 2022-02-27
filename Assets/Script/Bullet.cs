using Script.Base;
using UnityEngine;

namespace Script
{
    public class Bullet : MonoBehaviour
    {
        public int Atk;
        public int CritRate;
        public int CritAtk;

        public void OnEnable()
        {
            var a = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
            Atk = a.Atk;
            CritAtk = a.CritAtk;
            CritRate = a.CritRate;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            { 
                gameObject.SetActive(false);
            }
            if (other.CompareTag("Wall"))
            { 
                gameObject.SetActive(false);
            }
        }
    }
}
