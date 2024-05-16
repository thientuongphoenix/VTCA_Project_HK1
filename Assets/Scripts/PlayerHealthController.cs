using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    //Tạo ra một "Thể hiện" tĩnh theo mô hình Singleton

    public int currentHealth, maxHealth;
    //Máu hiện tại và máu lớn nhất

    public float invincibleLength;
    //Người chơi sẽ không nhận sát thương trong bao lâu
    private float invincibleCounter;
    //Bộ đếm ngược từ lúc nhận sát thương đến hết hiệu ứng invincible

    private SpriteRenderer theSR;
    //Gọi Component SpriteRenderer

    public GameObject deathEffect;
    //Object deathEffect

    //Awake cập nhật theo thời gian mặc định được cài đặt trong Project Setting (0.2s)
    private void Awake() 
    {
        instance = this; 
        //Tham chiếu đến thể hiện hiện tại của lớp PlayerHealthController
    }

    void Start()
    {
        currentHealth = maxHealth; 
        //Bắt đầu trò chơi cho máu hiện tại bằng máu lớn nhất

        theSR = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            //trừ dần bộ đếm để về 0
            //Time.deltaTime là lượng thời gian cần thiết giữa các Frame cập nhật
            //Game có 60 Frame 1 giây thì số này sẽ là 1/60

            if(invincibleCounter <= 0 )
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
                //Hết thời gian hiệu lực Invicibility, trả về hình ảnh bình thường
            }
        }

    }

    public void DealDamage() //Hàm kích hoạt khi nhận sát thương
    {
        if(invincibleCounter <= 0) 
            //invincibleCounter <= 0 thì mới nhận sát thương
        {
            currentHealth -= 1;
            //Nhận sát thương thì trừ 1 máu

            if (currentHealth <= 0) //Nếu máu về 0
            {
                currentHealth = 0;

                //gameObject.SetActive(false);
                //Máu hiện tại bằng 0 sẽ vô hiệu hóa Object được gắn vào (Player)

                Instantiate(deathEffect, transform.position, transform.rotation);
                //Khởi tạo một bảng sao của Object death effect

                LevelManager.instance.RespawnPlayer();
                //Gọi hàm hồi sinh nhân vật
            }
            else //Nếu máu chưa về 0
            {
                invincibleCounter = invincibleLength;

                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                //Tạo hiệu ứng cho Invincibility

                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.UpdateHealthDisplay();
            //Gọi Script điều khiển UI cho thanh máu để cập nhật trạng thái máu
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        //Máu hiện tại +1

        if(currentHealth > maxHealth)
            //Nếu máu hiện tại lớn hơn máu lớn nhất
        {
            currentHealth = maxHealth;
            //Thì máu hiện tại = máu lớn nhất
        }

        UIController.instance.UpdateHealthDisplay();
        //Cập nhật lại hình ảnh thanh máu
    }
}
