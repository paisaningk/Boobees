using Assets.Script.scriptableobject;
using UnityEngine;

namespace Assets.Script
{
    public class TestSO : MonoBehaviour
    {
        [SerializeField]private CharacterSo adc;

        private string name;
        private int hp;
        private int Atk;
        private float Speed;

        public void Start()
        {
            name = adc.Name;
            hp = adc.MaxHp;
            Atk = adc.Atk;
            Speed = adc.Speed;
        }
    
        public void Update()
        {
            //adc.Print();
            Debug.Log(hp);
        }
    }
}
