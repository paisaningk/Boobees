using Assets.Script.Base;
using Assets.Script.scriptableobject.Item;
using UnityEngine;

namespace Assets.Script
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO ItemSo;

        private int MaxHp;
        private int Atk;
        private float Speed;

        private void Start()
        {
            MaxHp = ItemSo.MaxHp;
            Atk = ItemSo.Atk;
            Speed = ItemSo.Speed;

        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var Player = GameObject.Find("Ronin Player").GetComponent<PlayerCharacter>();
                Player.Hp += MaxHp;
                Player.Atk += Atk;
                Player.Speed += Speed;
                Debug.Log("Player pickup");
                
                Destroy(gameObject);
            }
        }
    }
}
