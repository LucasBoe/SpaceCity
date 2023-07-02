using UnityEngine;

public class FreeCameraRotation : MonoBehaviour
{
    [SerializeField] CustomCursor customCursor;

    public float rotationSpeed = 5f; // Adjust the rotation speed as desired
    public float rotationSmoothing = 0.1f; // Adjust the smoothing factor as desired

    private bool isRotating = false;


    private void Update()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButtonDown(1))
        {
            StartRotating();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopRotating();
        }

        // Rotate the camera if the right mouse button is held down
        if (isRotating)
        {
            Vector3 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Rotate the camera around the Y and X axes based on the mouse movement
            float rotationX = -mouseDelta.y * rotationSpeed;
            float rotationY = mouseDelta.x * rotationSpeed;

            // Smoothly interpolate the rotation
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x + rotationX, transform.eulerAngles.y + rotationY, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime / rotationSmoothing);
        }
    }
    private void StartRotating()
    {
        isRotating = true;
        customCursor.LockPosition();
        customCursor.SetMode(CustomCursorMode.Rotate);
    }
    private void StopRotating()
    {
        isRotating = false;
        customCursor.UnlockPosition();
        customCursor.ResetMode();
    }
}