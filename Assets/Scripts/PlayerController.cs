using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.0f;
    [SerializeField] private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    [SerializeField] private float desiredRotationSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        this.enabled = false;
	}
	public void EnableMovement()
    {
        this.enabled = true;
    }
	// Update is called once per frame
	void Update()
    {
        Move();
	}

    private void Move()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            playerAnimator.SetBool("Run",true);
            float horizontalAxis = Input.GetAxis("Horizontal");
            Vector3 moveDirection = new Vector3(horizontalAxis, 0, 0);
            LookAt();
            playerRigidbody.MovePosition(transform.position + moveDirection * movementSpeed * Time.deltaTime);
        }
        else
        {
            playerAnimator.SetBool("Run",false);
            playerAnimator.SetTrigger("Walk");
        }
    }
    private void LookAt()
    {
        Vector3 moveDirection = new Vector3();
        if(Input.GetAxis("Horizontal") != 0)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            moveDirection = new Vector3(horizontalAxis, 0, 0);
            
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        Quaternion toRotation = Quaternion.LookRotation(moveDirection,Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation, desiredRotationSpeed * Time.deltaTime);
    }
}
