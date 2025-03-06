using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Relaxation, DailyExercise, HighIntensity }
    public Difficulty selectedDifficulty;

    public Button startTrailButton;

    void Start()
    {
        startTrailButton.interactable = false;
    }

    public void SetDifficulty(int difficulty)
    {
        selectedDifficulty = (Difficulty)difficulty;
        startTrailButton.interactable = true;
        Debug.Log("Selected Difficulty: " + selectedDifficulty);
    }

    public void StartTrail()
    {
        Debug.Log("Starting Trail with Difficulty: " + selectedDifficulty);
        // Save the selected difficulty to use in the AR Trail Scene
        PlayerPrefs.SetString("SelectedDifficulty", selectedDifficulty.ToString());
        PlayerPrefs.Save();

        // Load the AR Trail Scene
        SceneManager.LoadScene("ARTrailScene");
    }
}