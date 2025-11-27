using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject player;
    public float speed = 50f;

    void Start()
    {
    }

    void Update()
    {
        if (player != null)
        {
            Move();
            PlayerGot();
        }
    }

    void Move()
    {
        Vector3 dir = transform.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position = Vector3.MoveTowards(transform.position,
            player.transform.position, speed * Time.deltaTime);
    }

    void PlayerGot()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 0.1f)
        {
            Destroy(gameObject);
            Debug.Log("Treasure collected!");
        }
    }
}
