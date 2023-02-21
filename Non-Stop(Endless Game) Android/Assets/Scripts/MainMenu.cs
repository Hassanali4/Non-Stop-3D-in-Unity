// This script manages the main menu screen and its functions.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // The text objects that display the number of coins and high score.
    public Text numberOfCoinesText;
    public Text highscoreText;
    private void Start()
    {
        // Set the text of the number of coins and high score to the values saved in the PlayerPrefs.
        numberOfCoinesText.text = PlayerPrefs.GetInt("NumberOfCoines").ToString();
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
    }

    private void Update()
    {
        // Stop the button sound effect from playing.
        FindObjectOfType<AudioManager>().Stop("button");
    }

    // This function loads the game level when the "Play" button is clicked.
    public void PlayGame()
    {
        // Play the button sound effect and load the game level scene.
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene("Level");
    }

    // This function quits the game when the "Quit" button is clicked.  
    public void Quit()
    {
        // Play the button sound effect and quit the application.
        FindObjectOfType<AudioManager>().Play("button");
        Application.Quit();
    }

}
