using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Sound;


public class EnemyArchor : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private Transform player;

    private float timeBtwShots;

    public GameObject projectile;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        var random = Random.Range(2, 5);
        timeBtwShots = random;
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }

        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(timeBtwShots <= 0)
        {
            SoundManager.Instance.Play(SoundManager.Sound.WitchAttack);
            Instantiate(projectile, transform.position, Quaternion.identity);
            var random = Random.Range(2, 5);
            timeBtwShots = random;
        }

        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
