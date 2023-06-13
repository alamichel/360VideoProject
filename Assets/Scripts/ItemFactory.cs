using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    private GameObject itemPrefab; // Reference to the hammer prefab
    private Camera mainCamera; // Reference to the main camera
    private float itemDistance = 1f; // Distance to the item from the camera

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (mainCamera != null && itemPrefab != null)
            {
                Vector3 itemPosition = mainCamera.transform.position + mainCamera.transform.forward * itemDistance;
                Instantiate(itemPrefab, itemPosition, mainCamera.transform.rotation);
            }
        }
    }
}
