using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Manager man;
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
        man = Manager.Instance;
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
            man.player.countS += exp;
            Destroy(bullet.gameObject);
            DropTreasure();
            Destroy(this.gameObject);
            Debug.Log("Exp: " + man.player.countS);
        }
    }

    void DropTreasure()
    {
        if (treasure != null)
        {
            Treasure t = Instantiate(treasure, transform.position, Quaternion.identity);
            t.player = man.player.gameObject;
        }
    }
}
