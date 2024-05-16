using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene, continueScene;
    //sử dụng kiểu string lưu tên Scene cần load

    public GameObject continueButton;

    void Start()
    {
        if(PlayerPrefs.HasKey(startScene + "_unlocked"))
            //Truy xuất dữ liệu để xác định có dữ liệu màn chơi đầu chưa 
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void StartGame()
        //Event NewGame
    {
        SceneManager.LoadScene(startScene);
        PlayerPrefs.DeleteAll(); 
        //Xóa toàn bộ dữ liệu trong hàm PlayerPrefs
    }

    public void ContinueGame()
        //Trường hợp có dữ liệu rồi 
    {
        SceneManager.LoadScene(continueScene);
        //Load Scene chỉ định bằng kiểu String
    }

    public void QuitGame()
    {
        Application.Quit();
        //Thoát App
        Debug.Log("Quitting Game");
    }
}
