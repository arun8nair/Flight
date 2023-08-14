using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8.0f;
    public float horizontalBound = 5.0f;
    public float upperVerticalBound = 10.0f;
    public float lowerVerticalBound = 5.0f;
    public float maxTiltAngleX = 25;
    public float maxTiltAngleZ = 45;
    private float hInput, vInput;
    private float sceneWidth;
    private Rigidbody rb;
    private GameManager gameManager;
    private Vector3 movement;
    private Vector3 startingMousePos;
    private Quaternion inputRotation = Quaternion.identity;
    private Transform propeller;
    private float propellerSpeed = 1000.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sceneWidth = Screen.width;
        propeller = FindPropeller(transform.GetChild(0));
    }
    void Update()
    {
        CaptureMovementInputs();
        CaptureTiltInputs();
    }

    void FixedUpdate()
    {
        Move();
        Tilt();
        RotatePropeller();
    }

    void CaptureMovementInputs()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        movement = new Vector3(hInput, vInput, 0);
    }

    void CaptureTiltInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            float mouseDragDistance = (startingMousePos - Input.mousePosition).x;
            if (mouseDragDistance != 0)     //to prevent snapping into unit rotation on mouse click
            {
                float mouseDragToAngle = mouseDragDistance / sceneWidth * 360;
                inputRotation = Quaternion.Euler(0, 0, mouseDragToAngle);
            }
        } else
        {   // automatic player tilt on moving sideways
            // limit the auto tilt angle to a range
            Quaternion targetRotation = Quaternion.Euler(maxTiltAngleX * -vInput, 0, maxTiltAngleZ * -hInput);
            inputRotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }
    }

    void Move()
    {
        Vector3 playerPosition = transform.position;
        // prevent player from going out of bounds
        playerPosition.x = Mathf.Clamp(playerPosition.x, -horizontalBound, horizontalBound);
        playerPosition.y = Mathf.Clamp(playerPosition.y, lowerVerticalBound, upperVerticalBound);

        rb.MovePosition(playerPosition + movement * Time.fixedDeltaTime * speed);
    }

    void Tilt()
    {
        rb.MoveRotation(inputRotation);
    }

    void RotatePropeller()
    {
        Rigidbody propellerRb = propeller.GetComponent<Rigidbody>();
        propellerRb.AddTorque(propeller.transform.right * propellerSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    Transform FindPropeller(Transform transform)
    {
        Transform prop = null;
        foreach(Transform tr in transform)
        {
            if(tr.CompareTag("Propeller"))
            {
                prop = tr;
            }
        }
        return prop;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            this.enabled = false;
            gameManager.GameOver();
            Debug.Log("Game Over!");
        }
    }
}
