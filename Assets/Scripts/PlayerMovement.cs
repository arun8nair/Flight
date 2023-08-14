using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float speed = 5.0f;
    public float horizontalBound = 5.0f;
    public float verticalBound = 5.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (hInput, vInput, 0);

        if(transform.position.x > horizontalBound)  // Bind player to play area
        {
            transform.position = new Vector3 (horizontalBound, transform.position.y, transform.position.z);
        } else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, transform.position.z);
        }
        if(rb.position.y > verticalBound)
        {
            transform.position = new Vector3 (transform.position.x, verticalBound, transform.position.z);
        } else if (rb.position.y < -verticalBound)
        {
            transform.position = new Vector3(transform.position.x, -verticalBound, transform.position.z);
        }
        rb.MovePosition(transform.position + movement * Time.deltaTime * speed);

    }
}
