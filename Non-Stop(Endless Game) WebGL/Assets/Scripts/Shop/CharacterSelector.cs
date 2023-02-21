using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
   // private CameraController cameraController;
    public int currentCharacterIndex = 0;
    public GameObject[] characters;
    void Awake()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject characters in characters)
            characters.SetActive(false);

        characters[currentCharacterIndex].SetActive(true);
       // cameraController._target = characters[currentCharacterIndex].GetComponent<Transform>();
    }
}
