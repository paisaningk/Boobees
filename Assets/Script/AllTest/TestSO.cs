using Assets.Script.scriptableobject;
using Assets.Script.scriptableobject.Character;
using UnityEngine;

namespace Assets.Script.AllTest
{
    public class TestSO : MonoBehaviour
    {
        [SerializeField]private CharacterSO adc;

        private string Name;
        private int Hp;
        private int Atk;
        private float Speed;

        public void Start()
        {
            Name = adc.Name;
            Hp = adc.MaxHp;
            Atk = adc.Atk;
            Speed = adc.Speed;
        }
    
        public void Update()
        {
            //adc.Print();
            Debug.Log(Hp);
        }
    }
}
