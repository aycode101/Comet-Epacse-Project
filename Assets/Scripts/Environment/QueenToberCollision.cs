using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: obstacles need a Box Collider component
// NOTE: attach this script to Queen Tobors only

public class QueenToborCollision : MonoBehaviour
{
    public GameObject player;
    private GameObject characterModel;
    //private GameObject mainCamera;
    private float playerGroundYPos;
    private static string word;
    private static string anadrome;
    private string userInput = "";

    void Start()
    {
        player = GameObject.Find("Player");
        characterModel = player.transform.Find("Ch42_nonPBR@Standard Run").gameObject;
        //mainCamera = player.transform.Find("Main Camera").gameObject;
        playerGroundYPos = player.transform.position.y;

        // GENERATE WORD ##############################
        // Ideas: txt file with a list of words to randomly choose from

        // Compute anadrome of word
        char[] charArr = word.ToCharArray();
        Array.Reverse(charArr);
        anadrome = new string(charArr);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only execute if collided with Player object (has tag "Player")
        if (other.gameObject.tag == "Player")
        {
            // Prevent collision from triggering infinitely
            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            // Make player stop and stand idle
            player.GetComponent<PlayerMove>().enabled = false;
            characterModel.GetComponent<Animator>().Play("Sad Idle");

            // Make player model fall to the ground if the collision was in mid-air
            while (player.transform.position.y > playerGroundYPos)
            {
                player.transform.Translate(new Vector3(0, -10f, 0) * Time.deltaTime);
            }

            // Start Queen Tobor puzzle
            StartCoroutine(Puzzle());

            // Resume running after player solves puzzle
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            player.GetComponent<PlayerMove>().enabled = true;
            characterModel.GetComponent<Animator>().Play("Standard Run");

        }
    }

    // Updates userInput
    public void getInput(string s)
    {
        userInput = s;
    }

    // Returns if user input is equal to the anadrome
    private bool InputIsCorrect(string input)
    {
        return String.Equals(input, anadrome, StringComparison.InvariantCultureIgnoreCase);
    }

    private IEnumerator Puzzle()
    {
        // Activate puzzle screen
        GameObject.Find("Canvas/PuzzleWord").GetComponent<TMPro.TextMeshProUGUI>().text = "" + word;
        
        GameObject.Find("Canvas/PuzzlePrompt").SetActive(true);
        GameObject.Find("Canvas/PuzzleWord").SetActive(true);
        GameObject.Find("Canvas/PuzzleInputField").SetActive(true);

        yield return null;

        // Keeps looping until user inputs correct answer
        while (!InputIsCorrect(userInput))
        {
            yield return null;
        }

        // Deactivate puzzle screen
        GameObject.Find("Canvas/PuzzlePrompt").SetActive(false);
        GameObject.Find("Canvas/PuzzleWord").SetActive(false);
        GameObject.Find("Canvas/PuzzleInputField").SetActive(false);

        // Call function to make queen tobor leave
        // - this.GetComponent<QUEEN_TOBOR_MOVEMENT_SCRIPT>().FUNCTION_NAME();

        yield return new WaitForSeconds(5); // Wait for queen tobor to leave

        yield return null;
    }
}
