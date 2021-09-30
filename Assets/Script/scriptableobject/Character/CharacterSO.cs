using UnityEngine;

namespace Assets.Script.scriptableobject.Character
{
    [CreateAssetMenu(menuName = "CharacterSO")]
    public class CharacterSO : ScriptableObject
    {
        public string Name;
        public int MaxHp;
        public int Atk;
        public float Speed;

        public void Print()
        {
            Debug.Log($"My Name is {Name} + MaxHp {MaxHp} + Atk {Atk} + speed {Speed}");
        }
    }
}
