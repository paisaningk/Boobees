using System.Collections;
using System.Collections.Generic;
using Script.Sound;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform StartFire;
    public float BulletSpeed = 3;

    public void FireBullet(Vector2 direction, float rotationZ,int ammo)
    {
        SoundManager.Instance.Play(SoundManager.Sound.Shot);
        if (ammo == 5)
        {
            SoundManager.Instance.Play(SoundManager.Sound.Ammo);
        }
        var bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
        bullet.SetActive(true);
        bullet.transform.position = StartFire.transform.position;
        var a = Random.Range(-3, 4);
        bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - a);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;
    }
}
