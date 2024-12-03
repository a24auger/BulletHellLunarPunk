using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent( typeof(Rigidbody2D))]

public class EnemyBulletShoot : MonoBehaviour
{
    private GameObject player;
    [SerializeField] EnemyStats enemyStats;
    private Rigidbody2D rb2D;
    private float currentRoF;
    private float runTime;
    

    void RandomizeRoF()
    {
        currentRoF = Random.Range(enemyStats.MinRof, enemyStats.MaxRof);
    }

    float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(Dest.y - rb2D.position.y, Dest.x - rb2D.position.x) + Mathf.Deg2Rad * (Random.Range(0, enemyStats.BulletSpread) - enemyStats.BulletSpread * 0.5f);
    }

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        RandomizeRoF();
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
        if (runTime >= currentRoF)
        {
            RandomizeRoF();
            runTime = 0;
            for(int i = 0; i < enemyStats.BulletAmount; i++)
            {
                float angle = AngleMath(player.GetComponent<Rigidbody2D>().position);
                Vector2 bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
                GameObject Bullet = Instantiate(enemyStats.BulletPrefab);
                Bullet.AddComponent(typeof(Bullet));
                Bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
                Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.enemy);
            }
        }
    }
}