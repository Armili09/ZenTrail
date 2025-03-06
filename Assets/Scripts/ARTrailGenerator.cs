using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrailGenerator : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    public GameObject trailMarkerPrefab;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private DifficultyManager.Difficulty selectedDifficulty;

    void Start()
    {
        // Retrieve the selected difficulty from PlayerPrefs
        string difficultyString = PlayerPrefs.GetString("SelectedDifficulty", "Relaxation");
        selectedDifficulty = (DifficultyManager.Difficulty)System.Enum.Parse(typeof(DifficultyManager.Difficulty), difficultyString);

        Debug.Log("Generating Trail for Difficulty: " + selectedDifficulty);
        GenerateTrail();
    }

    void GenerateTrail()
    {
        switch (selectedDifficulty)
        {
            case DifficultyManager.Difficulty.Relaxation:
                Debug.Log("Generating Relaxation Trail");
                // Place fewer markers or make the trail shorter
                break;
            case DifficultyManager.Difficulty.DailyExercise:
                Debug.Log("Generating Daily Exercise Trail");
                // Place a moderate number of markers
                break;
            case DifficultyManager.Difficulty.HighIntensity:
                Debug.Log("Generating High Intensity Trail");
                // Place more markers or make the trail longer
                break;
        }
    }

    void Update()
    {
        // Example: Place trail markers on touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(trailMarkerPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}