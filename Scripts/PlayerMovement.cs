using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;           // Increased default - feels better for a ball
    public Camera mainCamera;           // Drag your Main Camera here

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Auto-find camera if not assigned
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");   // A/D
        float moveVertical = Input.GetAxisRaw("Vertical");       // W/S

        // Get camera forward & right, but flatten them (no Y)
        Vector3 camForward = mainCamera.transform.forward;
        Vector3 camRight = mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        // Normalize so diagonal isn't faster
        camForward.Normalize();
        camRight.Normalize();

        // Create movement direction relative to camera
        Vector3 movement = (camForward * moveVertical) + (camRight * moveHorizontal);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            // score++;   ← you had this but score var is missing, I left it commented
        }
    }
}