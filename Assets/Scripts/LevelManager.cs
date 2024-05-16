using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToRespawn;
    //Thời gian chờ trước khi hồi sinh

    public int gemsCollected;
    //Bộ đếm số lượng Gem đã nhặt

    public string levelToLoad;
    //Tên của Scene cần Load

    public float timeInLevel;
    //Bộ đếm thời gian trong màn chơi

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
        //Hàm chờ đợi trong một khoảng thời gian (Coroutine)
    {
        PlayerController.instance.gameObject.SetActive(false);
        //Vô hiệu hóa Object Player

        AudioManager.instance.PlaySFX(8);

        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.instance.fadeSpeed));
        //yield return là cách trả về giá trị chờ hồi sinh
        //Sau khi hết thời gian chờ sẽ thực hiện lệnh tiếp theo

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 0.2f);

        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);
        //Bật lại Object Player

        PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;
        //Cập nhật lại vị trí của Object Player sau khi hồi sinh

        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        //Hồi lại đầy máu cho nhân vật sau khi hồi sinh
        UIController.instance.UpdateHealthDisplay();
        //Cập nhật lại hình ảnh thanh máu
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        AudioManager.instance.PlayLevelVictory();
        //

        PlayerController.instance.stopInput = true;

        CameraController.instance.stopFollow = true;

        UIController.instance.levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + 3f);
        //Trong thời gian chờ thì gọi Fade Screen để chuyển cảnh

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        //Lưu lại trạng thái đã mở khóa (1 là true) khi kết thúc Level (Tên Level + _unlocked)

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        //Lưu lại Scene hiện tại, dùng cho lưu vị trí Level hiện tại trên Select Level

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if(gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }
        //Lưu lại số lượng Gem tốt nhất của người chơi
        
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if(timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }
        //Lưu giá trị tốt nhất của thời gian hoàn thành màn chơi 

        SceneManager.LoadScene(levelToLoad);
        //Load Scene cần load
    }
}
