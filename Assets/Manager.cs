using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public Plane player;
    private Plane currentPlayer;

    public Layout1 l1;
    public Layout2 l2;
    public Layout3 l3;

    public int currentLv = 1;
    public bool checkLv1 = false;
    public bool checkLv3 = false;

    public UserSavePointDatas userSavePointDatas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Spawn nhân vật từ prefab được chọn
        if (CharactorSelector.selectedChar != null)
        {
            Vector3 spawnPoint = new Vector3(0, -4, 0);
            GameObject obj = Instantiate(CharactorSelector.selectedChar, spawnPoint, Quaternion.identity);
            currentPlayer = obj.GetComponent<Plane>();
        }
        else
        {
            Debug.LogError("Chưa chọn nhân vật trước khi vào game!");
            return;
        }

        // Layout setting
        if (l1 != null) l1.gameObject.SetActive(true);
        if (l2 != null) l2.gameObject.SetActive(false);
        if (l3 != null) l3.gameObject.SetActive(false);

        // Load điểm
        if (PlayerPrefs.HasKey(UserDataKey.POINT_KEY))
        {
            string jsonData = PlayerPrefs.GetString(UserDataKey.POINT_KEY);
            userSavePointDatas = JsonUtility.FromJson<UserSavePointDatas>(jsonData);
        }
        else
        {
            userSavePointDatas = new UserSavePointDatas();
        }

        userSavePointDatas.StartGame();
    }

    void Update()
    {
        if (currentPlayer == null)
        {
            Time.timeScale = 0;
            Debug.Log("Game Over!");
            return;
        }

        switch (currentLv)
        {
            case 1:
                if ((l1 != null && l1.EnemyinPos()) || checkLv1)
                {
                    currentPlayer.Shoot();
                    checkLv1 = true;
                }
                break;

            case 2:
                currentPlayer.Shoot();
                break;

            case 3:
                if ((l3 != null && l3.EnemyinPos()) || checkLv3)
                {
                    currentPlayer.Shoot();
                    checkLv3 = true;
                }
                break;
        }

        // Chuyển level
        if (currentLv == 1 && l1 != null && l1.LevelFinished())
        {
            currentPlayer.ClearBullet();
            checkLv1 = false;

            l1.gameObject.SetActive(false);
            if (l2 != null) l2.gameObject.SetActive(true);

            currentLv = 2;
        }
        else if (currentLv == 2 && l2 != null && l2.LevelFinished())
        {
            currentPlayer.ClearBullet();

            l2.gameObject.SetActive(false);
            if (l3 != null) l3.gameObject.SetActive(true);

            currentLv = 3;
        }
        else if (currentLv == 3 && l3 != null && l3.LevelFinished())
        {
            currentPlayer.ClearBullet();
            checkLv3 = false;

            l3.gameObject.SetActive(false);
            if (l1 != null) l1.gameObject.SetActive(true);

            currentLv = 1;
        }
    }

    public void AddStar(int amount)
    {
        currentPlayer.countS += amount;

        userSavePointDatas.UpdatePoints(currentPlayer.countS);

        string jsonData = JsonUtility.ToJson(userSavePointDatas);
        PlayerPrefs.SetString(UserDataKey.POINT_KEY, jsonData);
        PlayerPrefs.Save();
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
