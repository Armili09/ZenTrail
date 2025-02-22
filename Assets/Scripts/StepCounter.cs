using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepCounter : MonoBehaviour
{
    public TextMeshProUGUI stepText;
    public TextMeshProUGUI distanceText;

    private int stepCount = 0;
    private float distanceWalked = 0f;
    private Vector3 lastAcceleration;
    private bool isStepDetected = false;
    private float stepThreshold = 1.5f; // Adjust this value based on sensitivity

    void Start()
    {
        lastAcceleration = Input.acceleration;
    }

    void Update()
    {
        Vector3 currentAcceleration = Input.acceleration;
        float delta = currentAcceleration.magnitude - lastAcceleration.magnitude;

        if (delta > stepThreshold && !isStepDetected)
        {
            stepCount++;
            isStepDetected = true;
            distanceWalked += 0.762f; // Average step length in meters (adjust as needed)
            UpdateUI();
        }
        else if (delta < stepThreshold && isStepDetected)
        {
            isStepDetected = false;
        }

        lastAcceleration = currentAcceleration;
    }

    void UpdateUI()
    {
        stepText.text = "Steps: " + stepCount;
        distanceText.text = "Distance: " + distanceWalked.ToString("F2") + " m";
    }
}