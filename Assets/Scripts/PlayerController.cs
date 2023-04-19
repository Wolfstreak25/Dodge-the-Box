using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if(m_Horizontal != 0)
        {
            playerAnimator.SetBool("Run",true);
            float horizontalAxis = m_Horizontal;
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
        if(m_Horizontal != 0)
        {
            float horizontalAxis = m_Horizontal;
            moveDirection = new Vector3(horizontalAxis, 0, 0);
            
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
        Quaternion toRotation = Quaternion.LookRotation(moveDirection,Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation, desiredRotationSpeed * Time.deltaTime);
    }

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector2 m_Move;
    private float m_Horizontal;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        m_Horizontal = context.ReadValue<float>();
    }

    public void SetInputActiveState(bool gameIsPaused)
    {
        switch (gameIsPaused)
        {
            case true:
                playerInput.DeactivateInput();
                break;

            case false:
                playerInput.ActivateInput();
                break;
        }
    }
}
