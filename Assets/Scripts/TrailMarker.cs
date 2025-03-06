using UnityEngine;

public class TrailMarker : MonoBehaviour
{
    public Transform userTransform; // Reference to the AR Camera (user's position)

    void Update()
    {
        // Make the marker face the user
        if (userTransform != null)
        {
            transform.LookAt(userTransform);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Lock rotation to Y-axis
        }
    }
}