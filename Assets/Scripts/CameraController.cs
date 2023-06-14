using UnityEngine;

/// <summary>
/// <c>CameraController</c> contains the logic that allows users to zoom into/out of a given 360-degree video, as well
/// as being able to change the camera view by clicking and dragging the screen.
/// </summary>
public class CameraController : MonoBehaviour
{
    // The transition manager for when we switch scenes.
    [SerializeField] private SceneTransitionManager sceneManager;
    // The menu panel.
    [SerializeField] private GameObject panel;
    // The speed the viewer can move the camera.
    private const float CAMERA_SPEED = 5.0f;
    // The most zoomed out the camera can go.
    private const int MAX_FIELD_OF_VIEW = 60;
    // The most zoomed in the camera can go.
    private const int MIN_FIELD_OF_VIEW = 20;
    // Change camera speed according to zoom in/out.
    private float zoomAmendment = 1.0f;
    // Change dragging type.
    private bool isDragging = true;

    void Start() => ResetCameraView();

    void Update()
    {
        // Adjust camera speed by change zoom_amendment proportion to zoom in/out
        zoomAmendment = 1.0f - (MAX_FIELD_OF_VIEW - Camera.main.fieldOfView) / MAX_FIELD_OF_VIEW;

        // If press "C" reset camera position and rotation
        if (Input.GetKeyDown(KeyCode.C))
            transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        // If user press "V", change dragging type.
        if (Input.GetKeyDown(KeyCode.V))
            isDragging = !isDragging;

        // Move camera around as desired (if not currently changing scenes or with panel active).
        if (!panel.activeSelf && !sceneManager.IsTransitioning)
            ChangeCameraView();
    }

    /// <summary>
    /// Allows camera to rotate and zoom in/out.
    /// </summary>
    private void ChangeCameraView()
    {
        // Adjust camera speed by change zoom_amendment proportion to zoom in/out.
        zoomAmendment = 1.0f - (MAX_FIELD_OF_VIEW - Camera.main.fieldOfView) / MAX_FIELD_OF_VIEW;

        if (!isDragging)
        {
            // Move around automatically by moving the mouse around the screen, mouse goes up, camera goes up, etc.
            transform.RotateAround(transform.position, Vector3.down, zoomAmendment * (CAMERA_SPEED - 2f) * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, zoomAmendment * (CAMERA_SPEED - 2f) * Input.GetAxis("Mouse Y"));
        }
        else if (Input.GetMouseButton(0))
        {
            // Move around by clicking and dragging video with mouse.
            // Ref: <https://youtu.be/RxlQnPcOoYc> (03:53/09:37)
            transform.RotateAround(transform.position, Vector3.down, zoomAmendment * CAMERA_SPEED * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, zoomAmendment * CAMERA_SPEED * Input.GetAxis("Mouse Y"));
        }
        else
        {
            // Move around by pressing WASD.
            if (Input.GetKey(KeyCode.W))
                transform.RotateAround(transform.position, transform.right, -zoomAmendment * (CAMERA_SPEED + 45f) * Time.deltaTime);

            if (Input.GetKey(KeyCode.S))
                transform.RotateAround(transform.position, transform.right, zoomAmendment * (CAMERA_SPEED + 45f) * Time.deltaTime);

            if (Input.GetKey(KeyCode.A))
                transform.RotateAround(transform.position, Vector3.down, zoomAmendment * (CAMERA_SPEED + 40f) * Time.deltaTime);

            if (Input.GetKey(KeyCode.D))
                transform.RotateAround(transform.position, Vector3.down, -zoomAmendment * (CAMERA_SPEED + 40f) * Time.deltaTime);
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

    /// <summary>
    /// Restores default camera view (i.e. facing forward and zoomed out) when called.
    /// </summary>
    public void ResetCameraView()
    {
        // Set initial zoom for camera.
        // Ref: <https://gamedevbeginner.com/how-to-zoom-a-camera-in-unity-3-methods-with-examples/#zoom_camera>
        Camera.main.fieldOfView = MAX_FIELD_OF_VIEW;

        // Set initial camera position and rotation at the start.
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
