using UnityEngine;
using System.Collections;


public class LocationTracker : MonoBehaviour
{
    public float UserLatitude { get; private set; }
    public float UserLongitude { get; private set; }
    public float UserHeading { get; private set; }

    void Start()
    {
        StartCoroutine(StartLocationService());
        Input.compass.enabled = true;
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services not enabled by user.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Location service failed.");
            yield break;
        }

        Debug.Log("Location service initialized.");
    }

    void Update()
    {
        UserLatitude = Input.location.lastData.latitude;
        UserLongitude = Input.location.lastData.longitude;
        Debug.Log($"Latitude: {UserLatitude}, Longitude: {UserLongitude}");

        UserHeading = Input.compass.trueHeading;
        Debug.Log($"User heading: {UserHeading} degrees");
    }
}