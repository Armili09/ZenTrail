using TMPro;
using UnityEngine;

public class StepCounter : MonoBehaviour
{
    public TextMeshProUGUI stepText;
    public TextMeshProUGUI distanceText;
    public Animator characterAnimator; // Reference to the Animator

    public int stepCount = 0;
    public float distanceWalked = 0f;
    private Vector3 lastAcceleration;
    private bool isStepDetected = false;
    private float stepThreshold = 1.5f; // Adjust this value based on sensitivity

    private float timeSinceLastStep = 0f; // Track time since the last step
    private float idleThreshold = 10f; // Time in seconds to switch to Idle animation

    void Start()
    {
        lastAcceleration = Input.acceleration;

        // Ensure the Animator is set to Idle by default
        if (characterAnimator != null)
        {
            characterAnimator.Play("Pen_idle"); // Play the idle animation by default
        }
    }

    void Update()
    {
        Vector3 currentAcceleration = Input.acceleration;
        float delta = currentAcceleration.magnitude - lastAcceleration.magnitude;

        // Check for steps
        if (delta > stepThreshold && !isStepDetected)
        {
            stepCount++;
            isStepDetected = true;
            distanceWalked += 0.762f; // Average step length in meters (adjust as needed)
            UpdateUI();

            // Reset the inactivity timer
            timeSinceLastStep = 0f;

            // Switch to Walk animation
            if (characterAnimator != null)
            {
                characterAnimator.Play("Pen_walk"); // Play the walk animation
            }
        }
        else if (delta < stepThreshold && isStepDetected)
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

        lastAcceleration = currentAcceleration;
    }

    public void UpdateUI()
    {
        stepText.text = "" + stepCount;
        distanceText.text = "" + distanceWalked.ToString("F2") + " m";
    }
}