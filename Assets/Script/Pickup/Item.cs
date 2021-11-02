using System;
using Assets.Script.Base;
using Assets.scriptableobject.Item;
using UnityEngine;

namespace Assets.Script.Pickup
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemSO ItemSo;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private GameObject Popup;
        private int maxHp;
        private int atk;
        private float speed;
        private float dashCd;
        private float critAtk;
        private float critRate;
        private int price;
        private Tier tier;

        private void Start()
        {
            maxHp = ItemSo.MaxHp;
            atk = ItemSo.Atk;
            speed = ItemSo.Speed;
            dashCd = ItemSo.DashCd;
            critAtk = ItemSo.CritAtk;
            critRate = ItemSo.CritRate;
            tier = ItemSo.Tier;
            sprite.sprite = ItemSo.Sprite;
            ShowPrice();
            //price = ItemSo.Price;
            //PrintAll();
        }
        


        private void PrintAll()
        {
            Debug.Log($"MaxHp {maxHp}");
            Debug.Log($"ATK {atk}");
            Debug.Log($"Speed {speed}");
            Debug.Log($"DashCd {dashCd}");
            Debug.Log($"CritAtk {critAtk}");
            Debug.Log($"critRate {critRate}");
            Debug.Log($"tier {tier}");
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerGold = other.GetComponent<PlayerCharacter>().Gold;
                
            }
        }

        private void ShowPrice()
        {
            switch (tier)
            {
                case Tier.Common:
                    price = 20;
                    break;
                case Tier.Uncommon:
                    price = 30;
                    break;
                case Tier.Rare:
                    price = 40;
                    break;
                case Tier.Epic:
                    price = 50;
                    break;
                case Tier.Cursed:
                    price = 44;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var textMesh = Popup.GetComponent<TextMesh>();
            textMesh.text = $"{price}";
        }

        private void PickUp(Collider2D other)
        {
            var Player = other.GetComponent<PlayerCharacter>();
            //Player.ItemSo.Add
            Player.Hp += maxHp;
            Player.Atk += atk;
            Player.Speed += speed;
            Player.DashCd += dashCd;
            Player.CritAtk += critAtk;
            Player.CritRate += critRate;
            Debug.Log("Player pickup");
                
            Destroy(gameObject);
        }
    }
}
