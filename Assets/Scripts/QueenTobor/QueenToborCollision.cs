using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

// NOTE: obstacles need a Box Collider component
// NOTE: attach this script to Queen Tobors only

public class QueenToborCollision : MonoBehaviour
{
    public GameObject player;
    public TextAsset wordFile;
    private GameObject characterModel;
    //private GameObject mainCamera;
    private float playerGroundYPos;
    private static string word;
    private static string anadrome;
    private string userInput = "";
    private GameObject canvas;

    void Start()
    {
        player = GameObject.Find("Player");
        characterModel = player.transform.Find("Ch42_nonPBR@Standard Run").gameObject;
        //mainCamera = player.transform.Find("Main Camera").gameObject;
        playerGroundYPos = player.transform.position.y;
        canvas = GameObject.Find("Canvas");

        // Generate word: txt file with a list of words to randomly choose from
        string[] words = Regex.Split(wordFile.text, "\n");
        word = words[Random.Range(0, words.Length)].ToUpper();

        // Compute anadrome of word
        char[] charArr = word.ToCharArray();
        Array.Reverse(charArr);
        anadrome = new string(charArr);

        Console.WriteLine("Chosen word: " + word);
        Console.WriteLine("Anadrome: " + anadrome);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only execute if collided with Player object (has tag "Player")
        if (other.gameObject.tag == "Player")
        {
            // Start Queen Tobor puzzle
            StartCoroutine(PuzzleSequence());
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
        Debug.Log("user input = " + userInput + "; correct answer = " + anadrome);
        return String.Equals(userInput, anadrome, StringComparison.InvariantCultureIgnoreCase);
    }

    private IEnumerator PuzzleSequence()
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

        yield return null;

        // Activate puzzle screen
        canvas.transform.Find("PuzzlePrompt").gameObject.SetActive(true);
        canvas.transform.Find("PuzzleWord").gameObject.SetActive(true);
        canvas.transform.Find("PuzzleInputField").gameObject.SetActive(true);

        GameObject.Find("Canvas/PuzzleWord").GetComponent<TMPro.TextMeshProUGUI>().text = "" + word;

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

        yield return new WaitForSeconds(3); // Wait for queen tobor to leave

        // Resume running after player solves puzzle
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        player.GetComponent<PlayerMove>().enabled = true;
        characterModel.GetComponent<Animator>().Play("Standard Run");

        yield return null;
    }
}