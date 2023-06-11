using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The speed the viewer can move the camera.
    private const float CAMERA_SPEED = 5.0f;
    // The most zoomed out the camera can go.
    private const int MAX_FIELD_OF_VIEW = 60;
    // The most zoomed in the camera can go.
    private const int MIN_FIELD_OF_VIEW = 20;

    private void Start()
    {
        // Set initial zoom for camera.
        // Ref: <https://gamedevbeginner.com/how-to-zoom-a-camera-in-unity-3-methods-with-examples/#zoom_camera>
        Camera.main.fieldOfView = MAX_FIELD_OF_VIEW;
    }

    void Update()
    {
        // Move around by clicking and dragging video with mouse.
        // Ref: <https://youtu.be/RxlQnPcOoYc> (03:53/09:37)
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(transform.position, Vector3.down, CAMERA_SPEED * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, CAMERA_SPEED * Input.GetAxis("Mouse Y"));
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

