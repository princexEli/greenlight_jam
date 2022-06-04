using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Manager : MonoBehaviour
{
    public bool isIso;
    public float moveSpeed = 5,rotationSpeed = 5;

    //2d only
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //iso only
    private Collider awareness;
    private Rigidbody rb;
    private GameObject capsule;

    // Start is called before the first frame update
    void Awake()
    {
		if (isIso) 
        {
            rb = gameObject.GetComponent<Rigidbody>();
            GameObject temp = gameObject.transform.Find("").gameObject;
            awareness = temp.GetComponent<Collider>();
            capsule = gameObject.transform.Find("Capsule").gameObject;
        }
        else
		{
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection;
        if (isIso)
		{
            movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
            
           
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
		else
        {
            movementDirection = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
            flip(horizontalInput);
            animator.SetFloat("movement", Mathf.Abs(horizontalInput + verticalInput));
        }

        Helper.ExitGame();
    }

    private void flip(float horizontalInput)
	{
        if (spriteRenderer.flipX && horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (!spriteRenderer.flipX && horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lootable")
        {
            other.gameObject.GetComponent<Loot>().Highlight(true);
        }else if (other.tag == "Level")
		{
            rb.velocity = Vector3.zero;
		}
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Lootable")
		{
            other.gameObject.GetComponent<Loot>().Highlight(false);
		}
    }
}
