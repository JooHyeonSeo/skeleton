using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    public Vector3 startSet;

    public void Start()
    {
        startSet = this.transform.position - player.transform.position;
    }

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 moveVector = new Vector3(playerPos.x * cameraSpeed * Time.deltaTime, playerPos.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.position = Vector3.Lerp(transform.position, playerPos + startSet, 8f * Time.deltaTime);
    }
}