using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Manager : MonoBehaviour
{
    public bool isIso;
    public float moveSpeed = 5;

    //2d only
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    //iso only
    

    // Start is called before the first frame update
    void Start()
    {
		if (isIso) 
        { 
            Debug.Log("isIso"); 
        }
        else
		{
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
		
        float verticalInput = Input.GetAxis("Vertical");
       
        if (isIso) 
        {
            transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
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
}
