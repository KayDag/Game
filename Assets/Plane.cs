using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Plane : MonoBehaviour
{
    public int speedMove = 1;
    public Vector3 moveInput;
    public List<GameObject> fired = new List<GameObject>();
    public List<Gun> GunPower = new List<Gun>();
    public List<int> score = new List<int> { 999, 5, 9, 13 };
    public int countS = 0;
    public int level = 1;
    public GameObject currentChar; // nhân vật hiện tại
    public SpriteRenderer spriteRenderer; // nếu dùng sprite

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TypeBullet(ref level);
    }
    void Move()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.z = 0;
        if (moveInput != Vector3.zero)
        {
            Vector3 direction = moveInput.normalized;
            transform.position += direction * speedMove * Time.deltaTime;
            MaxScreen();
        }
    }
    void MaxScreen()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float minX = cam.transform.position.x - camWidth / 2f;
        float maxX = cam.transform.position.x + camWidth / 2f;
        float minY = cam.transform.position.y - camHeight / 2f;
        float maxY = cam.transform.position.y + camHeight / 2f;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX + 0.5f, maxX - 0.5f);
        pos.y = Mathf.Clamp(pos.y, minY + 0.5f, maxY - 0.5f);
        transform.position = pos;
    }
    void TypeBullet(ref int level)
    {
        if (level < GunPower.Count && countS == score[level - 1])
        {
            level++;
        }
    }
    public void Shoot()
    {
        if (level - 1 >= GunPower.Count) return;

        List<GameObject> newBullets = GunPower[level - 1].fire();
        if (newBullets != null && newBullets.Count > 0)
        {
            fired.AddRange(newBullets);
        }
    }

    public void ClearBullet()
    {
        foreach (GameObject f in fired)
        {
            if (f != null) Destroy(f);
        }
        fired.Clear();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Destroy(gameObject);
            Debug.Log("Game Over!!!");
            Time.timeScale = 0;
        }
    }
    public void SetCharacter(GameObject charPrefab)
    {
        currentChar = charPrefab;

        // Nếu chỉ thay sprite
        SpriteRenderer charSprite = charPrefab.GetComponent<SpriteRenderer>();
        if (charSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = charSprite.sprite;
        }

        // Nếu muốn instantiate prefab lên plane
        // GameObject newChar = Instantiate(charPrefab, transform.position, Quaternion.identity, transform);
    }
}
