using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;
    //Tạo một mảng chứa toàn bộ SoundEffect

    public AudioSource bgm, levelEndMusic;
    //bgm (Background music)

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        //Dừng âm thanh trước để phát tiếp

        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);
        //Thay đổi cao độ cho âm thanh tránh nhàm chán

        soundEffects[soundToPlay].Play();
    }

    public void PlayLevelVictory()
    {
        bgm.Stop();
        levelEndMusic.Play();
    }
}
