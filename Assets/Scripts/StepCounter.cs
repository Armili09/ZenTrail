using TMPro;
using UnityEngine;

public class StepCounter : MonoBehaviour
{
    public TextMeshProUGUI stepText;
    public TextMeshProUGUI distanceText;
    public Animator characterAnimator; // Reference to the Animator

    public int stepCount = 0;
    public float distanceWalked = 0f;
    private Vector3 lowPassValue; // Smoothed accelerometer data
    private bool isStepDetected = false;
    private float stepThreshold = 1.2f; // Sensitivity for step detection
    private float lowPassFilterFactor = 0.2f; // Smoothing factor for accelerometer data

    private float timeSinceLastStep = 0f; // Track time since the last step
    private float idleThreshold = 10f; // Time in seconds to switch to Idle animation
    private float stepCooldown = 0.5f; // Minimum time between steps (in seconds)
    private float timeSinceLastStepDetection = 0f; // Cooldown timer

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
            }
        }

        // Initialize the low-pass filter
        lowPassValue = Input.acceleration;

        // Ensure the Animator is set to Idle by default
        if (characterAnimator != null)
        {
            characterAnimator.Play("Pen_idle"); // Play the idle animation by default
        }

        if (!SystemInfo.supportsAccelerometer)
        {
            Debug.LogError("Accelerometer not supported on this device!");
            // Handle the lack of accelerometer (e.g., show a message to the user)
        }
    }

    void Update()
    {
        // Apply a low-pass filter to smooth the accelerometer data
        lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, lowPassFilterFactor);

        // Log accelerometer data for debugging
        Debug.Log("Acceleration: " + Input.acceleration);
        Debug.Log("Low-Pass Value: " + lowPassValue);

        // Calculate the magnitude of the smoothed acceleration
        float accelerationMagnitude = lowPassValue.magnitude;

        // Check for steps with a cooldown
        timeSinceLastStepDetection += Time.deltaTime;

        if (accelerationMagnitude > stepThreshold && !isStepDetected && timeSinceLastStepDetection >= stepCooldown)
        {
            stepCount++;
            isStepDetected = true;
            distanceWalked += 0.762f; // Average step length in meters (adjust as needed)
            UpdateUI();

            // Reset the inactivity timer
            timeSinceLastStep = 0f;

            // Reset the cooldown timer
            timeSinceLastStepDetection = 0f;

            // Switch to Walk animation
            if (characterAnimator != null)
            {
                characterAnimator.Play("Pen_walk"); // Play the walk animation
            }
        }
        else if (accelerationMagnitude < stepThreshold && isStepDetected)
        {
            isStepDetected = false;
        }

        // Update the inactivity timer
        timeSinceLastStep += Time.deltaTime;

        // Switch to Idle animation if the user has been inactive for the threshold time
        if (timeSinceLastStep >= idleThreshold && characterAnimator != null)
        {
            characterAnimator.Play("Pen_idle"); // Play the idle animation
        }
    }

    public void UpdateUI()
    {
        stepText.text = "" + stepCount;
        distanceText.text = "" + distanceWalked.ToString("F2") + " m";
    }
}