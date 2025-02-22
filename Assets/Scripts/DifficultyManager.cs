using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public OSRMDirections osrmDirections;
    public LocationTracker locationTracker;

    public void OnEasyButtonClicked()
    {
        GenerateTrail(0.005f, 0.005f);
    }

    public void OnMediumButtonClicked()
    {
        GenerateTrail(0.01f, 0.01f);
    }

    public void OnHardButtonClicked()
    {
        GenerateTrail(0.02f, 0.02f);
    }

    void GenerateTrail(float latOffset, float lngOffset)
    {
        float startLat = locationTracker.UserLatitude;
        float startLng = locationTracker.UserLongitude;
        float endLat = startLat + latOffset;
        float endLng = startLng + lngOffset;

        osrmDirections.FetchDirections(startLat, startLng, endLat, endLng);
    }
}