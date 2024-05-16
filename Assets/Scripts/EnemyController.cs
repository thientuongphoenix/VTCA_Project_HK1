using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    //Tốc độ di chuyển

    public Transform leftPoint, rightPoint;
    //Vị trí dừng bên trái và phải

    private bool movingRight;
    //true thì đi sang phải, false thì đi sang trái

    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    private Animator anim;

    public float moveTime, waitTime;
    //Thời gian kẻ địch di chuyển và thời gian dừng
    private float moveCount, waitCount;
    //Bộ đếm thời gian di chuyển và dừng

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;
        //Khi bắt đầu trò chơi, leftPoint và rightPoint không là Object con nữa

        movingRight = true;
        //Khởi tạo giá trị cho movingRight

        moveCount = moveTime;
        //Khởi tạo giá trị đầu tiên cho moveCount
    }

    void Update()
    {
        if(moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (movingRight)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                //Vận tốc di chuyển trên trục X

                theSR.flipX = true;
                //Quay hình ảnh con ếch sang phía đối diện

                if (transform.position.x > rightPoint.position.x)
                    //Giới hạn phạm vi di chuyển
                {
                    movingRight = false;
                }
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                theSR.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if(moveCount <= 0)
                //Hết thời gian di chuyển thì chuyển sang thời gian dừng
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true); 
        }
        else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * .75f, moveTime * 1.25f);
                //Hết thời gian dừng thì tiếp tục di chuyển
            }
            anim.SetBool("isMoving", false);
        }
    }
}
