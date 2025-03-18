using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnDestroy()
    {
        // Destroy the parent of the GameObject this script is attached to
        Destroy(gameObject);
    }
}

