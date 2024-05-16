using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target; //Lấy vị trí của Object được chỉ định (Object Player)

    public Transform farBackground, middleBackground; //Lấy vị trí của Background

    public float minHeight, maxHeight; //Thông số giới hạn di chuyển của trục Y

    public bool stopFollow;

    //private float lastXPos; //Lấy vị trí cuối cùng của trục X làm vị trí cho Camera
    private Vector2 lastPos; //Lấy vị trí cuối cùng của trục X và Y

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //lastXPos = transform.position.x; 
        //Gán giá trị trục X lúc bắt đầu cho lastXPos

        lastPos = transform.position;
        //Gán giá trị trục X và Y lúc bắt đầu cho lastPos
    }

    void Update()
    {
        if (!stopFollow)
        {
            /* 
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        //Trục x và y lấy vị trí của Object Player, z lấy vị trí mặc định của Camera (Vị trí ban đầu của Camera)

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        //Sử dụng hàm Clamp để xác định giới hạn cho trục Y

        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        //Cập nhật lại giá trị trục Y
        */

            //Viết lại 3 dòng code trong 1 dòng
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            //float amountToMoveX = transform.position.x - lastXPos;
            //Lấy khoảng cách trục X hiện tại trừ cho khoảng cách trục X trước đó 
            //Mục đích là để tạo ra một thông số cho trục X của Camera nhỏ hơn trục X của nhân vật từ đó nó di chuyển chậm hơn

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);

            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f; //Tốc độ giảm 1/2

            //lastXPos = transform.position.x;
            lastPos = transform.position; //Cập nhật lại vị trí
        }        
    }
}
