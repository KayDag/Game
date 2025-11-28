using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Manager man;
    public float speed = 5f;

    void Start()
    {
        man = Manager.Instance;
    }

    void Update()
    {
        if (man.player != null)
        {
            Move();
            PlayerGot();
        }
    }

    void Move()
    {
        Vector3 dir = transform.position - man.player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position = Vector3.MoveTowards(transform.position,
            man.player.transform.position, speed * Time.deltaTime);
    }

    public void PlayerGot()
    {
        if (Vector3.Distance(transform.position, man.player.transform.position) < 0.1f)
        {
            man.player.countS++;
            Destroy(gameObject);
            Debug.Log("Treasure collected!");
        }
    }
}
