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
        m_MobileInput.SetActive(false);
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

    internal enum ControlStyle
    {
        None,
        KeyboardMouse,
        Touch,
        GamepadJoystick,
    }
    internal ControlStyle m_ControlStyle;
    public GameObject m_MobileInput;

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

    // This is called when PlayerInput updates the controls bound to its InputActions.
    public void OnControlsChanged()
    {
        // We could determine the types of controls we have from the names of the control schemes or their
        // contents. However, a way that is both easier and more robust is to simply look at the kind of
        // devices we have assigned to us. We do not support mixed models this way but this does correspond
        // to the limitations of the current control code.

        if (playerInput.GetDevice<Touchscreen>() != null) // Note that Touchscreen is also a Pointer so check this first.
            m_ControlStyle = ControlStyle.Touch;
        else if (playerInput.GetDevice<Pointer>() != null)
            m_ControlStyle = ControlStyle.KeyboardMouse;
        else if (playerInput.GetDevice<Gamepad>() != null || playerInput.GetDevice<Joystick>() != null)
            m_ControlStyle = ControlStyle.GamepadJoystick;
        else
            Debug.LogError("Control scheme not recognized: " + playerInput.currentControlScheme);

        // Enable button for main menu depending on whether we use touch or not.
        // With kb&mouse and gamepad, not necessary but with touch, we have no controls.
        if ((playerInput.GetDevice<Touchscreen>() != null))
        {
            m_MobileInput.SetActive(true);
        }
    }
}
