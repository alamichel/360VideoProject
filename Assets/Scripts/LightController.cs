using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private void Update()
    {
        // Update directional light rotation to match camera rotation
        transform.rotation = cameraTransform.rotation;
    }
}
