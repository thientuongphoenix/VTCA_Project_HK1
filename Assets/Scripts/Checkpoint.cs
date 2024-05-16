using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR; 
    //Component SpriteRenderer

    public Sprite cpOn, cpOff;
    //Hai biến thay đổi hình ảnh Checkpoint

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            //Tìm kiếm va chạm với Collier có tag Player
        {
            CheckpointController.instance.DeactiveCheckPoints();
            //Off các Checkpoint trước, On Checkpoint vừa chạm

            theSR.sprite = cpOn;
            //Nếu có thì bật hình của cpOn

            CheckpointController.instance.SetSpawnPoint(transform.position);
            //Gọi SetSpawnPoint với tham số là vị trí của Checkpoint đang bật
        }
    }

    public void ResetCheckPoint()
        //Hàm thay đổi hình ảnh Checkpoint sang Off
    {
        theSR.sprite = cpOff;
    }
}
