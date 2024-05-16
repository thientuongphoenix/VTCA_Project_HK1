using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHealth;
    //Đây có phải là Gem không ?

    private bool isCollected;
    //Có nhặt vật phẩm hay chưa ?
    //Mục đích của biến này là vì đôi khi hệ thống vật lý xác định va chạm 2 lần
    //Vô tình nhặt 1 Gem thành 2 Gem

    public GameObject pickupEffect;
    //Object PickupEffect

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
                //Nếu là Gem
            {
                LevelManager.instance.gemsCollected++;
                //Tăng bộ đếm Gem nhặt được lên 1

                isCollected = true;
                //Nhặt rồi thì không cho nhặt nữa
                Destroy(gameObject);
                //Hủy đối tượng Gem sau khi nhặt

                Instantiate(pickupEffect, transform.position, transform.rotation);
                //Khởi tạo một bảng sao PickupEffect tại vị trí Gem

                UIController.instance.UpdateGemCount();
                //Cập nhật hình ảnh UI đếm Gem

                AudioManager.instance.PlaySFX(6);
            }

            if (isHealth)
            {
                if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.HealPlayer();
                    //Gọi hàm hồi máu cho nhân vật

                    isCollected = true;
                    //Nhặt rồi thì không cho nhặt nữa
                    Destroy(gameObject);
                    //Hủy đối tượng Gem sau khi nhặt

                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    //Khởi tạo một bảng sao PickupEffect tại vị trí Health

                    AudioManager.instance.PlaySFX(7);
                }
            }
        }      
    }
}
