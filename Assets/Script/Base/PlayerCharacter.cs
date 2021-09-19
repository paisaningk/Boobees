﻿using Assets.Script.scriptableobject;
using UnityEngine;

namespace Assets.Script.Base
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSO PlayerCharacterSo;
    
        private string Name;
        private int Hp;
        public int Atk;
        private float Speed;
        

        public void Start()
        {
            Name = PlayerCharacterSo.Name;
            Hp = PlayerCharacterSo.MaxHp;
            Atk = PlayerCharacterSo.Atk;
            Speed = PlayerCharacterSo.Speed;
            //PrintAll();
        }

        public void PrintAll()
        {
            //Debug.Log("Enemy");
            Debug.Log($"name:{Name}");
            Debug.Log($"HP:{Hp}");
            Debug.Log($"ATK:{Atk}");
            Debug.Log($"Speed:{Speed}");
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyHitBox"))
            {
                var a = GameObject.Find(other.name).GetComponentInParent<EnemyCharacter>();
                var atkPlayer = a.Atk;
                Hp -= atkPlayer;
                if (Hp <= 0)
                {
                    gameObject.SetActive(false); 
                }
                Debug.Log($"{Name} have : {Hp}");
            }
        }
    }
}