using UnityEngine;

namespace Assets.Script.AllTest
{
    public class Hittest : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("hit");
        }
    }
}
