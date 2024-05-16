using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //Tạo ra một "Thể hiện" tĩnh

    public float moveSpeed; //Tốc độ di chuyển
    public Rigidbody2D theRB; //Component giả lập vật lý
    public float jumpForce; //Tốc độ nhảy

    private bool isGrounded; //Xác định có chạm đất hay không
    public Transform groundCheckPoint; //Biến lấy vị trí Transform
    public LayerMask whatIsGround; //Biến lấy tên Layer cần xác định va chạm

    private bool canDoubleJump; //Biến xác định có thể kích hoạt nhảy lần 2

    private Animator anim; //Bộ quản lý hoạt ảnh
    private SpriteRenderer theSR; //Component Sprite Renderer

    public float knockBackLength, knockBackForce; 
    //Lực tác động trong bao lâu và khoảng cách ép người chơi lùi lại
    private float knockBackCounter; 
    //Bộ đếm ngược để bắt đầu và kết thúc quá trình knock back

    public float bounceForce;
    //Đạp đầu kẻ địch thì hất tung nhân vật

    public bool stopInput;
    //Dùng cho LevelEnd

    private void Awake()
    {
        instance = this;
    }

    // Gọi ở Frame đầu tiên
    void Start()
    {
        anim = GetComponent<Animator>();//Gọi Component Animator
        theSR = GetComponent<SpriteRenderer>(); //Gọi Component Sprite Renderer
    }

    // Gọi mỗi Frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            //Phần trong ngoặc nhọn chỉ diễn ra khi knockBackCounter <= 0
            {
                theRB.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), theRB.velocity.y);
                //Tham số cho velocity là 1 vector 2 với x là vận tốc di chuyển * đầu vào và y là tốc độ hiện tại của trục y (y sẽ không thay đổi)

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
                //groundCheckPoint.position là vị trí xác định va chạm, .2f là bán kính, LayerMask (Lấy tên của Layer cần xác định va chạm khi bị chồng lên)

                if (isGrounded)
                {
                    canDoubleJump = true;
                    //Biến luôn true khi nhân vật đứng trên mặt đất
                }

                if (Input.GetButtonDown("Jump"))
                //nút Space gắn với hành động jump được nhấn xuống thì mới nhảy
                {
                    if (isGrounded) //Điều kiện nhảy
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        //giữ nguyên trục x, thay đổi trục y
                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                            canDoubleJump = false;
                            //Biến trở về false khi đã kích hoạt nhảy lần 2
                            AudioManager.instance.PlaySFX(10);
                        }
                    }
                }

                //Điều kiện để nhân vật quay mặt theo hướng chạy (Dùng chức năng FlipX của Rigidbody 2D)
                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                //Bộ đếm ngược trừ dần về 0
                if (!theSR.flipX) //Nhân vật quay mặt bên phải
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else //Nhân vật quay mặt bên trái
                {
                    theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                }
            }
        }
        

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x)); 
        //Xác định điều kiện chuyển đổi hoạt ảnh, gồm chuỗi tên của điều kiện và giá trị x (Vector2)
        //Mathf.Abs là lấy trị tuyệt đối, để nhân vật tiến hay lùi đều kích hoạt hoạt ảnh

        anim.SetBool("isGrounded", isGrounded); 
        //Xác định điều kiện chuyển đổi hoạt ảnh, gồm chuỗi tên của điều kiện và giá trị bool 
    }

    public void KnockBack()
        //Hàm này được gọi trong PlayerHealthController.DealDamage
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
        //Thay trục X = 0 -> Nhân vật lập tức đứng yên
        //Thay trục Y = knockBackForce -> Nhân vật nhảy lên 1 đoạn ngắn

        anim.SetTrigger("hurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
