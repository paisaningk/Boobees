using UnityEngine;

namespace Assets.Script.scriptableobject.Item
{
    [CreateAssetMenu(menuName = "ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int maxHp;
        public int atk;
        public float speed;
        // public enum Type
        // {
        //     Attack,
        //     movespeed,
        //     
        // }
    }
}
