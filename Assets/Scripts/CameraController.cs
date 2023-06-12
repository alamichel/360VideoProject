using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _cameraObject;
    [SerializeField] private int _cameraSpeed = 5;
    [SerializeField] private GameObject panel;

    // Update is called once per frame
    void Update()
    {
        // don't move camera if panel is active
        if (panel.activeSelf)
        {
            return;
        }
        // Move around by dragging video with mouse
        // Ref: <https://youtu.be/RxlQnPcOoYc> (03:53/09:37)
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(transform.position, Vector3.down, _cameraSpeed * Input.GetAxis("Mouse X"));
            transform.RotateAround(transform.position, transform.right, _cameraSpeed * Input.GetAxis("Mouse Y"));
        }
    }
}

