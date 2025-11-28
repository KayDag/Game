using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public UserSavePointDatas userSavePointDatas;
    public CharactorSelector selectedChar;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
            // Lấy nhân vật đã chọn
            if (CharactorSelector.selectedChar != null)
            {
                player.SetCharacter(CharactorSelector.selectedChar);
            }
        player.transform.position = new Vector3(0, -4, 0);
        if (l1 != null) l1.gameObject.SetActive(true);
        if (l2 != null) l2.gameObject.SetActive(false);
        if (l3 != null) l3.gameObject.SetActive(false);
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
        if (player)
        {
            // --- Shoot logic theo level ---
            switch (currentLv)
            {
                case 1:
                    if ((l1 != null && l1.EnemyinPos()) || checkLv1)
                    {
                        player.Shoot();
                        checkLv1 = true;
                    }
                    break;
                case 2:
                    player.Shoot();
                    break;
                case 3:
                    if ((l3 != null && l3.EnemyinPos()) || checkLv3)
                    {
                        player.Shoot();
                        checkLv3 = true;
                    }
                    break;
            }

            // --- Level transitions ---
            if (currentLv == 1 && l1 != null && l1.LevelFinished())
            {
                player.ClearBullet();
                checkLv1 = false;
                l1.gameObject.SetActive(false);
                if (l2 != null) l2.gameObject.SetActive(true);
                currentLv = 2;
            }
            else if (currentLv == 2 && l2 != null && l2.LevelFinished())
            {
                player.ClearBullet();
                l2.gameObject.SetActive(false);
                if (l3 != null) l3.gameObject.SetActive(true);
                currentLv = 3;
            }
            else if (currentLv == 3 && l3 != null && l3.LevelFinished())
            {
                player.ClearBullet();
                checkLv3 = false;
                l3.gameObject.SetActive(false);
                if (l1 != null) l1.gameObject.SetActive(true);
                currentLv = 1;  // quay lại layout1
            }
        }
        else
        {
            // Game over
            Time.timeScale = 0;
            Debug.Log("Game Over!");
        }
    }

    public void PauseGame()
    {
        Debug.Log("Menu");
        Time.timeScale = 0;
    }
    public void AddStar(int amount)
    {
        player.countS += amount;

        // Update dữ liệu
        userSavePointDatas.UpdatePoints(player.countS);

        // Lưu vào PlayerPrefs
        string jsonData = JsonUtility.ToJson(userSavePointDatas);
        PlayerPrefs.SetString(UserDataKey.POINT_KEY, jsonData);
        PlayerPrefs.Save();

        Debug.Log("Star added! Current star point: " + player.countS);
    }
}
