using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrailGenerator : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject trailMarkerPrefab;
    public Transform userTransform; // AR Camera (user's position)

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private List<GameObject> trailMarkers = new List<GameObject>();

    public TrailGuide trailGuide;

    void Update()
    {
        // Example: Place a trail marker on touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject marker = Instantiate(trailMarkerPrefab, hitPose.position, hitPose.rotation);
                trailMarkers.Add(marker);

                // Set the user transform for the marker to face the user
                TrailMarker trailMarkerScript = marker.GetComponent<TrailMarker>();
                if (trailMarkerScript != null)
                {
                    trailMarkerScript.userTransform = userTransform;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject marker = Instantiate(trailMarkerPrefab, hitPose.position, hitPose.rotation);
                trailMarkers.Add(marker);

                // Set the next marker for the TrailGuide
                if (trailGuide != null)
                {
                    trailGuide.SetNextMarker(marker.transform);
                }
            }
        }
    }    

    public void ClearTrail()
    {
        foreach (GameObject marker in trailMarkers)
        {
            Destroy(marker);
        }
        trailMarkers.Clear();
    }
}