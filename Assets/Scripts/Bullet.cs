using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 9f;
    private Rigidbody bulletRigidbody;

    public GameObject effect;

    public AudioClip sound1;
    public AudioClip sound2;


    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                //
                playerController.cnt--;
                playerController.UpdateUI();


                audio.PlayOneShot(sound1);
                if (playerController.cnt == 0)
                {
                    Instantiate(effect, transform.position, Quaternion.identity);
                    playerController.Die();
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
