using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    public GameManager gameManager;
    public float speed = 20f;

    public float jumpForce = 10f;  //������ ���� ��

    public VariableJoystick joy; // ����������̽�ƽŸ���̶�� joy��� ������ ����

    private bool isGrounded = true; // �ٴڿ� ����ִ��� ����

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

        float xInput = joy.Horizontal; //�¿찪
        float zInput = joy.Vertical;  // �յڰ�

        float xSpeed = xInput * speed;
        float ySpeed = playerRigidbody.velocity.y;
        float zSpeed = zInput * speed;

        // ���� ó��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpCount < 2)
        {
            Jump();
        }

        Vector3 newVelocity = new Vector3(xSpeed, ySpeed, zSpeed);
        playerRigidbody.velocity = newVelocity;
    }

    // ���� �Լ�
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

    // �÷��̾ �ٴڿ� ����� �� ȣ��Ǵ� �Լ�
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
