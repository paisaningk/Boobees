using Script.Sound;
using UnityEngine;

namespace Script.Controller
{
    public class GunController : MonoBehaviour
    {
        public Transform startFire;
        public float bulletSpeed = 3;

        public void FireBullet(Vector2 direction, float rotationZ,int ammo)
        {
            SoundManager.Instance.Play(SoundManager.Sound.Shot);
            if (ammo == 5)
            {
                SoundManager.Instance.Play(SoundManager.Sound.Ammo);
            }
            var bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
            bullet.SetActive(true);
            bullet.transform.position = startFire.transform.position;
            var range = Random.Range(-3, 4);
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - range);
            
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}
