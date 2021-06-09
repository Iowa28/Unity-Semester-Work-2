using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    // Controller speed properties.
    [Header("Speed Settings")]
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float sprintSpeed;

    [Header("Jump Settings")]
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpForce;

    // Controller key properties.
    [Header("Key Settings")]
    [SerializeField]
    private KeyCode walkKey;

    [SerializeField]
    private KeyCode sprintKey;

    // Stored required properties.
    private float speed;
    private Vector2 input;
    private float verticalVelocity;

    // Stored required components.
    private CharacterController characterController;

    void Awake() 
    {
        characterController = GetComponent<CharacterController>();
        cameraController.Initialization(transform);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        cameraController.Update();
        
        ReadInput();
        CalculateSpeed();
        ApplyMovement();
    }

    void ReadInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Left game");
        }
    }

    void CalculateSpeed()
    {
        if (Input.GetKey(walkKey))
        {
            speed = walkSpeed;
        } else if (Input.GetKey(sprintKey))
        {
            speed = sprintSpeed;
        } else {
            speed = runSpeed;
        }
    }

    void ApplyMovement()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else 
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }


        Vector3 moveVector = Vector3.zero;
        Vector3 forward = transform.forward * input.y * speed;
        Vector3 horizontal = transform.right * input.x * speed;
        moveVector = forward + horizontal;
        moveVector.y = verticalVelocity;

        characterController.Move(moveVector * Time.deltaTime);

    }
}
