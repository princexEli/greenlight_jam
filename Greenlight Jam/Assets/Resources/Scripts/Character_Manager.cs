using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Manager : MonoBehaviour
{
    #region instance
    private static Character_Manager instance;
    public static Character_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Character_Manager>();
            }

            return instance;
        }
        set { instance = value; }
    }
    #endregion

    public float moveSpeed, rotationSpeed;

    //2d only
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //iso only
    bool isIso;
    private Collider awareness;
    private Rigidbody rb;
    private GameObject capsule;
    
    bool canLoot = false;
    bool canExit = false;
    Exit exit;
    Loot lootObj;


    // Start is called before the first frame update
    void Awake()
    {
        if (Helper.SceneType() == "Pause")
            isIso = true;
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

    void Update()
	{
        if (canLoot && lootObj != null && Input.GetButtonDown("Interact"))
		{
            lootObj.interact();
		}
        else if(canExit && Input.GetButtonDown("Interact"))
		{
            exit.teleport();
		}
	}

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
            canLoot = true;
            lootObj = other.gameObject.GetComponent<Loot>();
            lootObj.Highlight(true);
        }else if (other.tag == "Level")
		{
            rb.velocity = Vector3.zero;
		}else if(other.tag == "EntranceExit")
		{
            canExit = true;
            exit = other.gameObject.GetComponent<Exit>();
            exit.Highlight(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Lootable")
		{
            cancelHighlight();
            canLoot = false;
		}
        else if (other.tag == "EntranceExit")
        {
            cancelExit();
            canExit = false;
        }
    }

    public void cancelHighlight()
	{
        if (lootObj == null) return;
        lootObj.Highlight(false);
        lootObj = null;
    }

    public void cancelExit()
    {
        if (exit == null) return;
        exit.Highlight(false);
        exit = null;
    }

    public void teleport(Transform newPos)
	{
        gameObject.transform.position = newPos.position;
	}
}
