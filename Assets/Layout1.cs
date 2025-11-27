using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Layout1 : MonoBehaviour
{
    public Plane player;
    public GameObject Enemy1;
    public Transform pos1;
    public GameObject Enemy2;
    public Transform pos2;
    public GameObject Enemy3;
    public Transform pos3;
    public GameObject Enemy4;
    public Transform pos4;
    public GameObject Enemy5;
    public Transform pos5;
    public GameObject Enemy6;
    public Transform pos6;
    public float speedB = 10;
    public float speedM = 15f;
    public float speedT = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(Enemy1, pos1, speedB);
        Move(Enemy2, pos2, speedB);
        Move(Enemy3, pos3, speedM);
        Move(Enemy4, pos4, speedM);
        Move(Enemy5, pos5, speedT);
        Move(Enemy6, pos6, speedT);
    }
    void Move(GameObject enemy, Transform pos, float speed)
    {
        if (enemy == null || pos == null) return;
        Vector3 dir = pos.position - enemy.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, 
            pos.position, speed * Time.deltaTime);        
        if (Vector3.Distance(enemy.transform.position, pos.position) < 0.05f)
        {
            enemy.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
    public bool LevelFinished()
    {
        return (Enemy1 == null && Enemy2 == null && Enemy3 == null &&
            Enemy4 == null && Enemy5 == null && Enemy6 == null);
    }
    public bool EnemyinPos()
    {
        if (Enemy1 == null || Enemy2 == null || Enemy3 == null ||
            Enemy4 == null || Enemy5 == null || Enemy6 == null)
            return false;

        return (Vector3.Distance(Enemy1.transform.position, pos1.position) < 0.05f &&
                Vector3.Distance(Enemy2.transform.position, pos2.position) < 0.05f &&
                Vector3.Distance(Enemy3.transform.position, pos3.position) < 0.05f &&
                Vector3.Distance(Enemy4.transform.position, pos4.position) < 0.05f &&
                Vector3.Distance(Enemy5.transform.position, pos5.position) < 0.05f &&
                Vector3.Distance(Enemy6.transform.position, pos6.position) < 0.05f);
    }
}
