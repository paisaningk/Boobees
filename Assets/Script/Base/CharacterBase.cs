using UnityEngine;

namespace Assets.Script.Base
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected string Name { get; set; }
        public int Hp { get; protected set; }
        public int Atk { get; protected set; }
        public float MovementSpeed { get; private set; }

        protected void SetCharact(string name,int hp,int atk,float movementSpeed)
        {
            Name = name;
            Hp = hp;
            Atk = atk;
            MovementSpeed = movementSpeed;
        }
    
        public abstract void OnTriggerEnter2D(Collider2D other);
    
    }
}
