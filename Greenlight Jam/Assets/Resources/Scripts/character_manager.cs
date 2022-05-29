using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class character_manager : MonoBehaviour
{

    public float moveSpeed = 5;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(spriteRenderer.flipX && horizontalInput > 0)
		{
            spriteRenderer.flipX = false;
        }
        else if(!spriteRenderer.flipX && horizontalInput < 0)
		{
            spriteRenderer.flipX = true;
        }
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
        animator.SetFloat("movement", Mathf.Abs(horizontalInput+ verticalInput));
    }
}
