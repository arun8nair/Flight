using UnityEngine;

public class FloatBalloon : MonoBehaviour
{
    private float speed = 100.0f;
    private float freq = 2;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Float();
    }

    void Float()
    {   
        Vector3 movement = Vector3.up * Time.fixedDeltaTime * speed * Mathf.Sin(Time.time * freq);
        rb.velocity = movement;
    }
}
