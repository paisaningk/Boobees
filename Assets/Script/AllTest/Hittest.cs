using UnityEngine;

namespace Assets.Script
{
    public class Hittest : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("hit");
        }
    }
}
