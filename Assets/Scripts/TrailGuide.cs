using UnityEngine;

public class TrailGuide : MonoBehaviour
{
    public Transform userTransform; // AR Camera (user's position)
    public Transform nextMarker;   // Next trail marker
    public GameObject directionArrow; // Visual cue (e.g., an arrow)

    void Update()
    {
        if (nextMarker != null)
        {
            // Calculate the direction to the next marker
            Vector3 direction = (nextMarker.position - userTransform.position).normalized;

            // Rotate the direction arrow to point to the next marker
            if (directionArrow != null)
            {
                directionArrow.transform.rotation = Quaternion.LookRotation(direction);
                directionArrow.transform.rotation = Quaternion.Euler(0, directionArrow.transform.rotation.eulerAngles.y, 0); // Lock rotation to Y-axis
            }

            Debug.Log("Move in direction: " + direction);
        }
        else
        {
            Debug.Log("Trail complete!");
            // Optionally, hide the direction arrow when the trail is complete
            if (directionArrow != null)
            {
                directionArrow.SetActive(false);
            }
        }
    }

    public void SetNextMarker(Transform marker)
    {
        nextMarker = marker;
        if (directionArrow != null)
        {
            directionArrow.SetActive(true);
        }
    }
}