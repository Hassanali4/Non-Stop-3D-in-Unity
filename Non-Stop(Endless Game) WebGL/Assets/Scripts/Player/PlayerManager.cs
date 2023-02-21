// Required libraries
using UnityEngine;
using UnityEngine.UI;

// Class to manage player data
public class PlayerManager : MonoBehaviour
{
    // Serialized field to allow editing in the Inspector
    [SerializeField] private GameObject _touchControlPanel;

    // Public variables
    public static bool _gameOver;// Static variable to check if the game is over
    public static bool _gamePause;// Static variable to check if the game is paused
    public GameObject gameOverPanel;// Game object for the Game Over panel
    public GameObject gamePausePanel;// Game object for the Game Pause panel
    [SerializeField] private Button _pauseButton;// Button for the Game Pause button
    public GameObject startingText;// Game object for the Starting text
    public static bool IsGameStarted;// Static variable to check if the game has started
    //public static bool isGameOver = false;// Static variable to check if the game is over

    public static int numberOfCoines;// Static variable to store the number of coins collected
    public Text numberOfCoinesText;// Text object to display the number of coins

    public static int highscore;// Static variable to store the high score
    public Text highscoreText;// Text object to display the high score
    public Text PausehighscoreText;// Text object to display the high score on the pause panel
    public Text NewhighscoreText;// Text object to display the new high score

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        numberOfCoines = PlayerPrefs.GetInt("NumberOfCoines",0);
        Time.timeScale = 1;
        _gameOver = false;
        _gamePause = false;
        IsGameStarted = false;

        // Set the touch control panel to active
        _touchControlPanel.gameObject.SetActive(true);

        // Initialize high score to 0
        highscore = 0;
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the game is over
        if (_gameOver)
        {
            Time.timeScale = 0;// Set the game timescale to 0
            _pauseButton.gameObject.SetActive(false);// Deactivate the pause button
            _touchControlPanel.gameObject.SetActive(false); // Deactivate the touch control panel
            gameOverPanel.SetActive(true);// Activate the Game Over panel
            _pauseButton.gameObject.SetActive(false);// Deactivate the pause button
        }

        // Update the number of coins collected
        numberOfCoinesText.text = numberOfCoines.ToString();

        // Check if the high score has been beaten
        if (PlayerPrefs.GetInt("Highscore") < numberOfCoines)
        {
            highscore = numberOfCoines;// Set the new high score
            PlayerPrefs.SetInt("Highscore", highscore);// Save the new high score to the PlayerPrefs
        }
        else 
        {
            highscore = PlayerPrefs.GetInt("Highscore");// Get the high score from the PlayerPrefs
            PausehighscoreText.text = highscore.ToString();// Update the high score text on the pause panel
            NewhighscoreText.text = highscore.ToString();// Update the new high score text
            highscoreText.text = highscore.ToString();// Update the high score text
        }

        // Check if the game has started
        if (Input.GetKeyDown(KeyCode.Space) || SwipeManager.tap)
        {
            IsGameStarted = true;// Set the game started flag to true
            Destroy(startingText);// Destroy the starting text object
        }
    }

    // Function to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1;// Set the game timescale to 1
        _gamePause = false; // Set the game pause flag to false
        gamePausePanel.SetActive(false);// Deactivate the game pause panel and reactivate the pause button
        _pauseButton.gameObject.SetActive(true);
    }
    public void PauseGame()
    {
        // Set isGameOver flag to true and gamePause flag to true
        Time.timeScale = 0;// Set the game timescale to 0
        _gamePause = true;
        // Activate the game pause panel and deactivate the pause button
        gamePausePanel.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
    }
}
