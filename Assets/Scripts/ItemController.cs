using UnityEngine;

public class ItemController : MonoBehaviour
{
    private float itemSpeed = 30f; // Speed of the item movement
    private float zChangeSpeed = 25f; // Speed of z-axis change
    private float itemDistance = 100f; // Distance to the item from the camera

    public Camera mainCamera;

    private bool isMoving = false; // Flag to indicate if the item is currently being moved
    private bool hasBeenClicked = false; // Flag to indicate if the item has been clicked before

    void Update()
    {
        // Move the item if the flag is set
        if (isMoving)
        {
            // Calculate the new position based on user input or any desired logic
            Vector3 newPosition = transform.position;

            // Move the item based on arrow key input
            if (Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += transform.up * itemSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                newPosition -= transform.up * itemSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition -= transform.right * itemSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += transform.right * itemSpeed * Time.deltaTime;
            }

            // Update the item's position
            transform.position = newPosition;

            // Change the item's z-axis position based on user input
            float zChange = 0f;
            if (Input.GetKey(KeyCode.P))
            {
                zChange = zChangeSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.L))
            {
                zChange = -zChangeSpeed * Time.deltaTime;
            }

            transform.position += new Vector3(0, 0, zChange);
        }
    }

    void OnMouseDown()
    {
        // Only allow the first item clicked to be moved
        if (!hasBeenClicked)
        {
            // Instantiate a copy of this item at a position in front of the camera
            if (mainCamera != null)
            {
                Vector3 itemPosition = mainCamera.transform.position + mainCamera.transform.forward * itemDistance;
                GameObject item = Instantiate(gameObject, itemPosition, mainCamera.transform.rotation) as GameObject;

                // Set the flag to indicate that the copied item can be moved
                item.GetComponent<ItemController>().isMoving = true;
            }

            // Set the flag to indicate that the item has been clicked to prevent double copy
            hasBeenClicked = true;
        }
    }
}
