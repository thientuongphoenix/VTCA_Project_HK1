using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
        //Hàm có sẵn của Unity dùng để xác định va chạm
    {
        if (other.tag == "Player") 
            //Tìm kiếm va chạm với Object mang tag Player
        {
            //Debug.Log("Hit");
            //Khi phát hiện va chạm, in ra Console "Hit"

            //FindObjectOfType<PlayerHealthController>().DealDamage();
            //Tìm Object PlayerHealthController và gọi hàm DealDamage

            PlayerHealthController.instance.DealDamage();
            //Thay vì phải tìm từng Object để xác định va chạm
            //Phương pháp này gọi ra thể hiện tĩnh "instance" từ bộ nhớ ngay lập tức
            //Hiệu suất cao hơn dùng FindObjectOfType
        }
    }
}
