using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject profileCanvas;
    public GameObject changeGoalsCanvas;
    public GameObject editProfileCanvas;

    public Button profileButton;
    public Button changeGoalsButton;
    public Button editProfileButton;
    public Button resetStepsButton;
    public Button changeGoalsDoneButton;
    public Button changeGoalsPlusButton;
    public Button changeGoalsMinusButton;
    public Button editProfileDoneButton;
    public Button profileBackButton;

    public TMP_InputField editUsernameInputField;
    public TextMeshProUGUI profileUsernameText;
    public TextMeshProUGUI mainMenuUsernameText;
    public TextMeshProUGUI changeGoalsValueText;

    private string currentUsername = "Username";
    private int currentGoalValue = 500;

    void Start()
    {
        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(true);
        if (profileCanvas != null) profileCanvas.SetActive(false);
        if (changeGoalsCanvas != null) changeGoalsCanvas.SetActive(false);
        if (editProfileCanvas != null) editProfileCanvas.SetActive(false);

        if (profileButton != null) profileButton.onClick.AddListener(OnProfileButtonClicked);
        if (changeGoalsButton != null) changeGoalsButton.onClick.AddListener(OnChangeGoalsButtonClicked);
        if (editProfileButton != null) editProfileButton.onClick.AddListener(OnEditProfileButtonClicked);
        if (resetStepsButton != null) resetStepsButton.onClick.AddListener(OnResetStepsButtonClicked);
        if (changeGoalsDoneButton != null) changeGoalsDoneButton.onClick.AddListener(OnChangeGoalsDoneButtonClicked);
        if (changeGoalsPlusButton != null) changeGoalsPlusButton.onClick.AddListener(OnChangeGoalsPlusButtonClicked);
        if (changeGoalsMinusButton != null) changeGoalsMinusButton.onClick.AddListener(OnChangeGoalsMinusButtonClicked);
        if (editProfileDoneButton != null) editProfileDoneButton.onClick.AddListener(OnEditProfileDoneButtonClicked);
        if (profileBackButton != null) profileBackButton.onClick.AddListener(OnProfileBackButtonClicked);

        // Add listener to input field's onValueChanged event
        if (editUsernameInputField != null)
        {
            editUsernameInputField.onValueChanged.AddListener(OnUsernameInputChanged);
        }

        UpdateUsernames();

        if (changeGoalsValueText != null)
        {
            UpdateGoalText();
        }
    }

    void OnProfileButtonClicked()
    {
        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(false);
        if (profileCanvas != null) profileCanvas.SetActive(true);
    }

    void OnProfileBackButtonClicked()
    {
        if (profileCanvas != null) profileCanvas.SetActive(false);
        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(true);
    }

    // Input field value changed event
    void OnUsernameInputChanged(string newUsername)
    {
        currentUsername = newUsername;
        UpdateUsernames();
    }

    void OnChangeGoalsButtonClicked()
    {
        if (profileCanvas != null) profileCanvas.SetActive(false);
        if (changeGoalsCanvas != null) changeGoalsCanvas.SetActive(true);
    }

    void OnEditProfileButtonClicked()
    {
        if (profileCanvas != null) profileCanvas.SetActive(false);
        if (editProfileCanvas != null) editProfileCanvas.SetActive(true);

        if (editUsernameInputField != null && profileUsernameText != null)
        {
            editUsernameInputField.text = profileUsernameText.text;
        }
    }

    void OnEditProfileDoneButtonClicked()
    {
        if (editProfileCanvas != null) editProfileCanvas.SetActive(false);
        if (profileCanvas != null) profileCanvas.SetActive(true);
    }

    void OnResetStepsButtonClicked()
    {
        Debug.Log("Reset Steps Clicked");
    }

    void OnChangeGoalsDoneButtonClicked()
    {
        if (changeGoalsCanvas != null) changeGoalsCanvas.SetActive(false);
        if (profileCanvas != null) profileCanvas.SetActive(true);
    }

    void OnChangeGoalsPlusButtonClicked()
    {
        currentGoalValue += 10;
        UpdateGoalText();
    }

    void OnChangeGoalsMinusButtonClicked()
    {
        currentGoalValue -= 10;
        if (currentGoalValue < 0) currentGoalValue = 0;
        UpdateGoalText();
    }

    void UpdateGoalText()
    {
        if (changeGoalsValueText != null)
        {
            changeGoalsValueText.text = currentGoalValue.ToString();
        }
    }

    void UpdateUsernames()
    {
        if (profileUsernameText != null) profileUsernameText.text = currentUsername;
        if (mainMenuUsernameText != null) mainMenuUsernameText.text = currentUsername;
    }
}