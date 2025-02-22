using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class OSRMDirections : MonoBehaviour
{
    private string baseUrl = "http://router.project-osrm.org/route/v1/walking/";

    public void FetchDirections(float startLat, float startLng, float endLat, float endLng)
    {
        string coordinates = $"{startLng},{startLat};{endLng},{endLat}";
        string url = $"{baseUrl}{coordinates}?overview=full";

        StartCoroutine(GetDirections(url));
    }

    IEnumerator GetDirections(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching directions: " + webRequest.error);
            }
            else
            {
                ProcessDirections(webRequest.downloadHandler.text);
            }
        }
    }

    void ProcessDirections(string jsonResponse)
    {
        OSRMResponse response = JsonUtility.FromJson<OSRMResponse>(jsonResponse);

        if (response.code == "Ok")
        {
            foreach (var route in response.routes)
            {
                Debug.Log($"Distance: {route.distance}, Duration: {route.duration}");
                foreach (var step in route.legs[0].steps)
                {
                    Debug.Log($"Step: {step.maneuver.instruction}, Distance: {step.distance}");
                }
            }
        }
        else
        {
            Debug.LogError("Directions request failed: " + response.code);
        }
    }

    [System.Serializable]
    public class OSRMResponse
    {
        public string code;
        public Route[] routes;
    }

    [System.Serializable]
    public class Route
    {
        public float distance;
        public float duration;
        public Leg[] legs;
    }

    [System.Serializable]
    public class Leg
    {
        public Step[] steps;
    }

    [System.Serializable]
    public class Step
    {
        public Maneuver maneuver;
        public float distance;
    }

    [System.Serializable]
    public class Maneuver
    {
        public string instruction;
    }
}