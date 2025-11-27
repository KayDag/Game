using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int speedBullet = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speedBullet * Time.deltaTime;
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float minX = cam.transform.position.x - camWidth / 2f;
        float maxX = cam.transform.position.x + camWidth / 2f;
        float minY = cam.transform.position.y - camHeight / 2f;
        float maxY = cam.transform.position.y + camHeight / 2f;
        Vector3 pos = transform.position;
        if (pos.x > maxX || pos.x < minX || pos.y > maxY || pos.y < minY)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Plane enemy = other.gameObject.GetComponent<Plane>();
        if (enemy != null)
        {
            Debug.Log("Destroy " + other.gameObject.name);
            Destroy(enemy.gameObject);
            Destroy(gameObject);
            Debug.Log("Shoot");
        }
    }
}
