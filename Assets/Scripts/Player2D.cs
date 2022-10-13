using UnityEngine;

public class Player2D : MonoBehaviour
{
    [Header("Player values")]
    [SerializeField, Range(0.0f, 10.0f)]
    float moveSpeed;
    [SerializeField, Range(0.0f, 10.0f)]
    float jumpForce;


    [Header("Ground Detection")]
    [SerializeField]
    Transform groundDetector;
    [SerializeField]
    LayerMask groundMask;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float desiredX = Input.GetAxis("Horizontal") * moveSpeed;

        float currentX = Vector3.Dot(rb.velocity, Vector3.right);
        float newX = Mathf.MoveTowards(currentX, desiredX, 15.0f);

        rb.velocity += Vector3.right * (newX - currentX);

        if (Input.GetButtonDown("Jump"))
        {
            if (GroundDetected())
            {
                rb.AddForce(Vector3.up * jumpForce * 100.0f);
            }
        }
    }

    bool GroundDetected()
    {
        Collider[] cols = Physics.OverlapSphere(groundDetector.position, 0.1f, groundMask);
        return cols.Length > 0;
    }
}
