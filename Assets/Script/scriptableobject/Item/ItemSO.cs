using UnityEngine;

namespace Assets.Script.scriptableobject.Item
{
    [CreateAssetMenu(menuName = "ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int MaxHp;
        public int Atk;
        public float Speed;
    }
}
