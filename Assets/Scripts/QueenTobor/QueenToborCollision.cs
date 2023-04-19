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
    public TextAsset wordFile;
    //public Behaviour createSection;
    //public Behaviour destroySection;

    private GameObject player;
    private GameObject characterModel;
    private float playerGroundYPos;
    private GameObject canvas;
    private float width;

    private static string word;
    private static string anadrome;

    void Start()
    {
        player = GameObject.Find("Player");
        characterModel = player.transform.Find("Ch42_nonPBR@Standard Run").gameObject;
        playerGroundYPos = player.transform.position.y;
        canvas = GameObject.Find("Canvas");
        width = this.GetComponent<Renderer>().bounds.size.x;

        // Generate word: txt file with a list of words to randomly choose from
        string[] words = Regex.Split(wordFile.text, "\n");
        word = words[Random.Range(0, words.Length)].ToUpper().TrimEnd('\r', '\n', ' '); ;

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

    // Returns if user input is equal to the anadrome
    private bool InputIsCorrect()
    {
        string input = canvas.transform.Find("PuzzleInputField").GetComponent<TMPro.TMP_InputField>().text;
        return String.Equals(input, anadrome, StringComparison.InvariantCultureIgnoreCase);
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

        // Pause scripts that generate/destroy sections
        //createSection.enabled = false;
        //destroySection.enabled = false;

        yield return null;

        // Activate puzzle screen
        canvas.transform.Find("PuzzlePrompt").gameObject.SetActive(true);
        canvas.transform.Find("PuzzleWord").gameObject.SetActive(true);
        canvas.transform.Find("PuzzleInputField").gameObject.SetActive(true);

        GameObject.Find("Canvas/PuzzleWord").GetComponent<TMPro.TextMeshProUGUI>().text = "" + word;

        yield return null;

        // Keeps looping until user inputs correct answer
        while (!InputIsCorrect())
        {
            yield return null;
        }

        // Deactivate puzzle screen
        GameObject.Find("Canvas/PuzzlePrompt").SetActive(false);
        GameObject.Find("Canvas/PuzzleWord").SetActive(false);
        GameObject.Find("Canvas/PuzzleInputField").SetActive(false);

        // Move Queen tobor out of the way
        float rightSide = transform.position.x + width;
        while (rightSide > LevelBoundary.leftSide)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 40);
            rightSide = transform.position.x + width;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return null;

        // Resume running after player solves puzzle
        player.GetComponent<PlayerMove>().enabled = true;
        characterModel.GetComponent<Animator>().Play("Standard Run");

        // Resume scripts that generate/destroy sections
        //createSection.enabled = true;
        //destroySection.enabled = true;

        yield return new WaitForSeconds(5);

        Destroy(gameObject);
        // If QueenTobors are generated through script:
        /*
         * if (transform.name == "QueenTobor(Clone)")
        {
            Destroy(gameObject);
        }
         */
    }
}