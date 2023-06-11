using UnityEngine;

/// <summary>
/// <c>CameraController</c> contains the logic that allows users to zoom into/out of a given 360-degree video, as well
/// as being able to change the camera view by clicking and dragging the screen.
/// </summary>
public class CameraController : MonoBehaviour
{
    // The speed the viewer can move the camera.
    private const float CAMERA_SPEED = 5.0f;
    // The most zoomed out the camera can go.
    private const int MAX_FIELD_OF_VIEW = 60;
    // The most zoomed in the camera can go.
    private const int MIN_FIELD_OF_VIEW = 20;
    // change camera speed according to zoom in/out
    private float zoom_amendment = 1.0f;
    // change dragging type
    private bool isDragging = true;

    private void Start()
    {
        // Set initial zoom for camera.
        // Ref: <https://gamedevbeginner.com/how-to-zoom-a-camera-in-unity-3-methods-with-examples/#zoom_camera>
        Camera.main.fieldOfView = MAX_FIELD_OF_VIEW;

        // Set initial camera position and rotation at the start.
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    void Update()
    {   
        // Adjust camera speed by change zoom_amendment proportion to zoom in/out
        zoom_amendment = 1.0f - (MAX_FIELD_OF_VIEW - Camera.main.fieldOfView) / MAX_FIELD_OF_VIEW;
        
        // If press "C" reset camera position and rotation
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        // if user press "V", change dragging type
        if (Input.GetKeyDown(KeyCode.V))
        {   
            isDragging = !isDragging;
        }

        // Move around by clicking and dragging video with mouse.
        // Ref: <https://youtu.be/RxlQnPcOoYc> (03:53/09:37)
        if (!isDragging)
        {
            // Move around automatically by moving the mouse around the screen, mouse goes up, camera goes up, etc.
            transform.RotateAround(transform.position, Vector3.down, zoom_amendment * (CAMERA_SPEED - 2f) * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, zoom_amendment * (CAMERA_SPEED - 2f) * Input.GetAxis("Mouse Y"));
        }
        else if (Input.GetMouseButton(0))
        {
            transform.RotateAround(transform.position, Vector3.down, zoom_amendment * CAMERA_SPEED * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, zoom_amendment * CAMERA_SPEED * Input.GetAxis("Mouse Y"));
        }
        else
        {   
            // Move around by pressing WASD.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.position, transform.right, -zoom_amendment * (CAMERA_SPEED + 45f) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.RotateAround(transform.position, transform.right, zoom_amendment * (CAMERA_SPEED + 45f) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.RotateAround(transform.position, Vector3.down, zoom_amendment * (CAMERA_SPEED + 40f) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAround(transform.position, Vector3.down, -zoom_amendment * (CAMERA_SPEED + 40f) * Time.deltaTime);
            }
        }

        // Zoom in/out of the view by scrolling.
        Camera.main.fieldOfView += Input.mouseScrollDelta.y;

        // Enforce limit on zooming out.
        if (Camera.main.fieldOfView > MAX_FIELD_OF_VIEW)
            Camera.main.fieldOfView = MAX_FIELD_OF_VIEW;

        // Enforce limit on zooming in.
        if (Camera.main.fieldOfView < MIN_FIELD_OF_VIEW)
            Camera.main.fieldOfView = MIN_FIELD_OF_VIEW;
    }
}

