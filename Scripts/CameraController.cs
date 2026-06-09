using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;           // Drag your Ball here

    [Header("Camera Settings")]
    public float distance = 8f;        // How far the camera stays from the ball
    public float height = 5f;          // How high the camera is above the ball
    public float mouseSensitivity = 180f;

    [Header("Vertical Limits")]
    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw = 0f;     // Left/Right rotation
    private float pitch = 20f;  // Up/Down tilt

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start behind the ball
        yaw = player.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;                    // Inverted Y is normal for cameras
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Calculate camera position around the ball
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = player.position + rotation * new Vector3(0, height, -distance);

        // Apply position and rotation
        transform.position = targetPosition;
        transform.LookAt(player.position);   // Always look at center of the ball
    }
}