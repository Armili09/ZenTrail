using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainMenuCanvas;
    public GameObject ProfileCanvas;
    public GameObject ChangeGoalsCanvas;
    public GameObject CongratsCanvas;

    public TextMeshProUGUI goalValueText;
    public StepCounter stepCounter;

    public Button profileButton; // On MainMenuCanvas
    public Button changeGoalsButton; // On ProfileCanvas
    public Button resetCounterButton; // On ProfileCanvas
    public Button backButton; // On ProfileCanvas and ChangeGoalsCanvas
    public Button plusButton; // On ChangeGoalsCanvas
    public Button minusButton; // On ChangeGoalsCanvas
    public Button doneButton; // On ChangeGoalsCanvas
    public Button congratsDoneButton; // On CongratsCanvas

    private int goalValue = 5; // Default goal value
    private bool isGoalReached = false; // Track if the goal has been reached

    void Start()
    {
        // Initialize canvases
        ShowMainMenuCanvas();
        CongratsCanvas.SetActive(false); // Ensure CongratsCanvas is hidden initially
        UpdateGoalText();

        // Add button listeners
        profileButton.onClick.AddListener(ShowProfileCanvas);
        changeGoalsButton.onClick.AddListener(ShowChangeGoalsCanvas);
        resetCounterButton.onClick.AddListener(OnResetCounterButtonPressed);
        backButton.onClick.AddListener(ShowMainMenuCanvas);
        plusButton.onClick.AddListener(() => OnChangeGoalsButtonPressed(1)); // Increment by 1
        minusButton.onClick.AddListener(() => OnChangeGoalsButtonPressed(-1)); // Decrement by 1
        doneButton.onClick.AddListener(OnDoneButtonPressed);
        congratsDoneButton.onClick.AddListener(OnCongratsDoneButtonPressed);
    }

    void Update()
    {
        // Check if the goal is reached and the CongratsCanvas is not already active
        if (stepCounter.stepCount >= goalValue && !isGoalReached)
        {
            isGoalReached = true;
            ShowCongratsCanvas();
        }
    }

    public void ShowMainMenuCanvas()
    {
        MainMenuCanvas.SetActive(true);
        ProfileCanvas.SetActive(false);
        ChangeGoalsCanvas.SetActive(false);
    }

    public void ShowProfileCanvas()
    {
        MainMenuCanvas.SetActive(false);
        ProfileCanvas.SetActive(true);
        ChangeGoalsCanvas.SetActive(false);
    }

    public void ShowChangeGoalsCanvas()
    {
        MainMenuCanvas.SetActive(false);
        ProfileCanvas.SetActive(false);
        ChangeGoalsCanvas.SetActive(true);
    }

    public void ShowCongratsCanvas()
    {
        // Show CongratsCanvas as an overlay without disabling other canvases
        CongratsCanvas.SetActive(true);
    }

    public void OnChangeGoalsButtonPressed(int change)
    {
        goalValue += change;
        if (goalValue < 1) goalValue = 1; // Ensure goal value doesn't go below 1
        UpdateGoalText();
    }

    public void OnDoneButtonPressed()
    {
        // Save the goal value (if needed) and return to ProfileCanvas
        ShowProfileCanvas();
    }

    public void OnCongratsDoneButtonPressed()
    {
        // Hide CongratsCanvas and allow the user to continue their previous activity
        CongratsCanvas.SetActive(false);
        isGoalReached = false; // Reset the goal reached flag

        // Reset the step count to prevent CongratsCanvas from reappearing immediately
        stepCounter.stepCount = 0;
        stepCounter.distanceWalked = 0f;
        stepCounter.UpdateUI();
    }

    public void OnResetCounterButtonPressed()
    {
        stepCounter.stepCount = 0;
        stepCounter.distanceWalked = 0f;
        stepCounter.UpdateUI();
    }

    private void UpdateGoalText()
    {
        goalValueText.text = goalValue.ToString();
    }
}