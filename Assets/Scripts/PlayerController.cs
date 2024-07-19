using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    public GameManager gameManager;
    public float speed = 20f;

    public float jumpForce = 10f;  //점프에 사용될 힘

    public VariableJoystick joy; // 베리어블조이스틱타입이라는 joy라는 변수를 만듬

    private bool isGrounded = true; // 바닥에 닿아있는지 여부

    public GameObject[] life; //
    public int cnt;

    private int jumpCount = 0; 

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cnt = 3;

    }

    // Update is called once per frame
    void Update()
    {
        //float xInput = Input.GetAxis("Horizontal");
        //float zInput = Input.GetAxis("Vertical");

        float xInput = joy.Horizontal; //좌우값
        float zInput = joy.Vertical;  // 앞뒤값

        float xSpeed = xInput * speed;
        float ySpeed = playerRigidbody.velocity.y;
        float zSpeed = zInput * speed;

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpCount < 2)
        {
            Jump();
        }

        Vector3 newVelocity = new Vector3(xSpeed, ySpeed, zSpeed);
        playerRigidbody.velocity = newVelocity;
    }

    // 점프 함수
    public void Jump()
    {
        if (jumpCount == 2)
        {
            return;
        }
        jumpCount++;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    // 플레이어가 바닥에 닿았을 때 호출되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    //
    public void UpdateUI()
    {
        for(int i=0; i<life.Length; i++)
        {
            life[i].SetActive(false);
        }
        for(int i=0; i<cnt; i++)
        {
            life[i].SetActive(true);
        }


    }

    public void Die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
