using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iso_character_manager : MonoBehaviour
{
    float moveSpeed = 5;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
    }
}
