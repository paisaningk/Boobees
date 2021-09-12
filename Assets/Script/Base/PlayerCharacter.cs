using Assets.Script.scriptableobject;
using UnityEngine;

namespace Assets.Script.Base
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterSo Player;
    
        private string name;
        private int hp;
        private int Atk;
        private float Speed;
        
        public void Start()
        {
            name = Player.Name;
            hp = Player.MaxHp;
            Atk = Player.Atk;
            Speed = Player.Speed;
            PrintAll();
        }

        public void PrintAll()
        {
            Debug.Log($"name:{name}");
            Debug.Log($"HP:{hp}");
            Debug.Log($"ATK:{Atk}");
            Debug.Log($"Speed:{Speed}");
        }
    }
}
