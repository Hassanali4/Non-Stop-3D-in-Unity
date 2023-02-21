using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public int currentCharacterIndex = 0;
    public Button buyButton;

    public GameObject[] characterModels;
    
    public PlayerBluePrint[] characters;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerBluePrint character in characters)
        {
            if (character.playerPrice == 0)
                character.isPlayerUnlocked = true;
            else
                character.isPlayerUnlocked = PlayerPrefs.GetInt(character.playerName, 0) == 0 ? false : true;
        }


        currentCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject characters in characterModels)
            characters.SetActive(false);

        characterModels[currentCharacterIndex].SetActive(true);
    }       

    // Update is called once per frame
    void Update()
    {
        UpdateUI();   
    }

    public void NextCharacter()
    {
        FindObjectOfType<AudioManager>().Play("button");
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex++;
        if (currentCharacterIndex > characterModels.Length - 1)
        {
            currentCharacterIndex = 0;
        }
        characterModels[currentCharacterIndex].SetActive(true);
        PlayerBluePrint c = characters[currentCharacterIndex];
        if (!c.isPlayerUnlocked)
            return;

        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }
    public void PreviousCharacter()
    {
        FindObjectOfType<AudioManager>().Play("button");
        characterModels[currentCharacterIndex].SetActive(false);
        currentCharacterIndex--;
        if (currentCharacterIndex == -1)
        {
            currentCharacterIndex = characterModels.Length - 1;
        }
        characterModels[currentCharacterIndex].SetActive(true);
        PlayerBluePrint c = characters[currentCharacterIndex];
        if (!c.isPlayerUnlocked)
            return;
        PlayerPrefs.SetInt("SelectedCharacter", currentCharacterIndex);
    }
    
    public void UnlockCharacter()
    {
        FindObjectOfType<AudioManager>().Play("button");
        PlayerBluePrint c = characters[currentCharacterIndex];
        c.isPlayerUnlocked = true;
        PlayerPrefs.SetInt("SelectedCharacter",currentCharacterIndex);
        PlayerPrefs.SetInt(c.playerName,1);
        PlayerPrefs.SetInt("NumberOfCoines",PlayerPrefs.GetInt("NumberOfCoines",0) - c.playerPrice);
    }

    private void UpdateUI()
    {
        FindObjectOfType<AudioManager>().Play("button");
        PlayerBluePrint c = characters[currentCharacterIndex];

        if (c.isPlayerUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else 
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<Text>().text = c.playerPrice.ToString();
            if (c.playerPrice < PlayerPrefs.GetInt("NumberOfCoines", 0))
                buyButton.interactable = true;
            else
                buyButton.interactable = false;
        }
    }
}
