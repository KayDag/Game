using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform EnemyGun;
    public Treasure treasure;   
    public float rSpawn = 1f;  
    public int freqTr = 1;      
    public float timer = 0;
    public float StaTime = 3;
    public float exp = 1;

    private void Start()
    {
        
    }
    void Update()
    {
        ShootEnemy();
    }
    public void ShootEnemy()
    {
        if (EnemyGun == null || EnemyBullet == null) return;
        timer += Time.deltaTime;
        if (timer >= StaTime)
        {
            Instantiate(EnemyBullet, EnemyGun.position, EnemyGun.rotation);
            timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
            DropTreasure();
            Destroy(this.gameObject);
        }
    }

    void DropTreasure()
    {
        if (treasure != null)
        {
            Treasure t = Instantiate(treasure, transform.position, Quaternion.identity);
        }
    }
}
