// Import required libraries
using UnityEngine;


// Define a serializable class for sound
public class Coine : MonoBehaviour
{// Update is called once per frame.
    void Update()
    {// Rotate the object around the x-axis at a speed of 50 units per second.
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }
    // This function is called when the object collides with another collider.
    private void OnTriggerEnter(Collider other)
    { // Check if the other collider is the player.
        if (other.tag == "Player")
        {
            // Play the "PickUpCoin" sound using the AudioManager.
            FindObjectOfType<AudioManager>().Play("PickUpCoin");
            // Increment the number of coins collected by the player.
            PlayerManager.numberOfCoines++;
            //Debug.Log("No of Coines : "+PlayerManager.numberOfCoines);
            // Save the updated number of coins collected in the PlayerPrefs.
            PlayerPrefs.SetInt("NumberOfCoines", PlayerManager.numberOfCoines);
            // Destroy the coin object.
            Destroy(gameObject);
        }
    }
}
