using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Layout3 : MonoBehaviour
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
    public Transform posT;
    public Transform posB;
    public bool allShoot = false;
    public bool check1 = false;
    public bool check2 = false;
    public bool check3 = false;
    public bool check4 = false;
    public float speedB = 15f;
    public float speedT = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(Enemy1, posT, pos1, speedB, ref check1);
        Move(Enemy2, posT, pos2, speedT, ref check2);
        Move(Enemy3, posB, pos3, speedT, ref check3);
        Move(Enemy4, posB, pos4, speedB, ref check4);
        if (!allShoot && EnemyinPos())
            allShoot = true;
    }
    void Move(GameObject enemy, Transform pos1, Transform pos2, float speed, ref bool check)
    {
        if (enemy == null || pos1 == null || pos2 == null) return;
        if (Vector3.Distance(enemy.transform.position, pos1.position) > 0.05f && !check)
        {
            Vector3 dir = pos1.position - enemy.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            enemy.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, 
                pos1.position, speed * Time.deltaTime);
        }
        if (Vector3.Distance(enemy.transform.position, pos1.position) < 0.05f)
        {
            check = true;
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (check)
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, 
                pos2.position, speed * Time.deltaTime);
    }
    public bool LevelFinished()
    {
        return (Enemy1 == null && Enemy2 == null && Enemy3 == null && Enemy4 == null);
    }
    public bool EnemyinPos()
    {
        if (Enemy1 == null || Enemy2 == null || Enemy3 == null || Enemy4 == null)
            return false;

        return (Vector3.Distance(Enemy1.transform.position, pos1.position) < 0.05f &&
                Vector3.Distance(Enemy2.transform.position, pos2.position) < 0.05f &&
                Vector3.Distance(Enemy3.transform.position, pos3.position) < 0.05f &&
                Vector3.Distance(Enemy4.transform.position, pos4.position) < 0.05f);
    }
}
