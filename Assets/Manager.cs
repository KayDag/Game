using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public Plane player;
    public Layout1 l1;
    public Layout2 l2;
    public Layout3 l3;

    public int currentLv = 1;
    public bool checkLv1 = false;
    public bool checkLv3 = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        if (l1 != null) l1.gameObject.SetActive(true);
        if (l2 != null) l2.gameObject.SetActive(false);
        if (l3 != null) l3.gameObject.SetActive(false);
    }

    void Update()
    {
        // Level 1 logic
        if (currentLv == 1 && ((l1 != null && l1.EnemyinPos()) || checkLv1))
        {
            player.Shoot();
            checkLv1 = true;
        }
        // Level 2 logic
        else if (currentLv == 2)
        {
            player.Shoot();
        }
        // Level 3 logic
        else if (currentLv == 3 && ((l3 != null && l3.EnemyinPos()) || checkLv3))
        {
            player.Shoot();
            checkLv3 = true;
        }

        // Level transitions
        if (currentLv == 1 && l1 != null && l1.LevelFinished())
        {
            player.ClearBullet();
            checkLv1 = false;
            l1.gameObject.SetActive(false);
            if (l2 != null) l2.gameObject.SetActive(true);
            currentLv++;
        }
        else if (currentLv == 2 && l2 != null && l2.LevelFinished())
        {
            player.ClearBullet();
            l2.gameObject.SetActive(false);
            if (l3 != null) l3.gameObject.SetActive(true);
            currentLv++;
        }
        else if (currentLv == 3 && l3 != null && l3.LevelFinished())
        {
            player.ClearBullet();
            Debug.Log("Win!!!");
        }

        // End game check
        if (player == null)
        {
            Time.timeScale = 0;
            Debug.Log("Game Over!");
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
