using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public List<Transform> gunPower;  
    public GameObject Bullet;        
    public float staTime = 3f;       
    private float timer = 0f;

    public List<GameObject> fire()
    {
        timer += Time.deltaTime;
        List<GameObject> spawned = new List<GameObject>();

        if (timer >= staTime)
        {
            foreach (var g in gunPower)
            {
                GameObject bullet = Instantiate(Bullet, g.position, g.rotation);
                spawned.Add(bullet);
            }
            timer = 0;
        }
        return spawned;
    }
}
