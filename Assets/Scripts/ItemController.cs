using UnityEngine;

public class HammerController : MonoBehaviour
{
    public float itemSpeed = 5f; // Speed of the hammer movement
    public float zChangeSpeed = 5f; // Speed of z-axis change

    void Update()
    {
        // Get the user input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Get the user input for vertical movement
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.P))
            transform.position += new Vector3(0f, 0f, zChangeSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.L))
            transform.position -= new Vector3(0f, 0f, zChangeSpeed * Time.deltaTime);

        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput, 0f);

        transform.Translate(movementDirection * itemSpeed * Time.deltaTime);
    }
}
