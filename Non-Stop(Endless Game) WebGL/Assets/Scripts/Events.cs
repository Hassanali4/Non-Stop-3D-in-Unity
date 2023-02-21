using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the game events, such as restarting the game, returning to the main menu, and quitting the game.
// It uses the Unity SceneManager class to load scenes.
// It also uses the FindObjectOfType method to find the AudioManager and play the "button" sound effect.
// The ReplayGame method is called when the player chooses to restart the game.
// The GameMenu method is called when the player chooses to return to the main menu.
// The QuitGame method is called when the player chooses to quit the game.

public class Events : MonoBehaviour
{

    public void ReplayGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        //GameObject.Find("CharacterHolder").GetComponentInChildren<Animator>().enabled = true;
        SceneManager.LoadScene("Level");
    }
    public void GameMenu()
    {
        FindObjectOfType<AudioManager>().Play("button");
        //GameObject.Find("CharacterHolder").GetComponentInChildren<Animator>().enabled = true;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        //GameObject.Find("CharacterHolder").GetComponentInChildren<Animator>().enabled = true;
        Application.Quit();
    }
}
