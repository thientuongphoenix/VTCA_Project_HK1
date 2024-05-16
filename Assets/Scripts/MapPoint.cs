using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;
    //Có phải Level không ? Có bị Lock không ?
    public string levelToLoad, levelToCheck, levelName;
    //String tên Level cần Load và kiểm tra cấp độ trước mở khóa chưa

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;
    //Object mini Icon trên MapPoint 

    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && levelToLoad != null)
        {
            if(PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems"); 
            }

            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            if (gemsCollected >= totalGems)
            {
                gemBadge.SetActive(true);
            }

            if (bestTime <= targetTime && bestTime != 0)
            {
                timeBadge.SetActive(true);
            }

            isLocked = true;

            if(levelToCheck != null)
            {
                if(PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }
        }
        //Mở khóa bằng cách check Level trước end chưa dựa trên hàm EndLevel()

        if(levelToLoad == levelToCheck)
        {
            isLocked=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
