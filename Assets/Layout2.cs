using UnityEditor;
using UnityEngine;

public class Layout2 : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject currentEnemy;
    public Transform pos;
    public float speed = 10f;
    public int freq = 10;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy == null)
        {
            if (count < freq)
                Spawn();
        }
        else
        {
            Move(currentEnemy, pos, speed);
        }

        EndGame();
    }


    void Spawn()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float leftX = cam.transform.position.x - camWidth / 2f;
        float rightX = cam.transform.position.x + camWidth / 2f;
        float topY = cam.transform.position.y + camHeight / 2f;
        float midY = cam.transform.position.y;

        float x = Random.Range(leftX, rightX);
        float y = Random.Range(midY + 5f, topY);

        currentEnemy = Instantiate(Enemy, new Vector3(x, y, 0), Quaternion.identity);
        count++;
    }

    void Move(GameObject enemy, Transform targetPos, float speed)
    {
        if (enemy == null || targetPos == null) return;

        Vector3 dir = targetPos.position - enemy.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        enemy.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos.position, speed * Time.deltaTime);
    }

    public bool LevelFinished()
    {
        return (count >= freq && currentEnemy == null);
    }

    void EndGame()
    {
        if (pos == null)
        {
            Time.timeScale = 0;
        }
    }
}