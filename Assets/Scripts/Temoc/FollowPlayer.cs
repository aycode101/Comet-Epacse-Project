using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;
    private Transform playerPosition;
    private PlayerMove playerMovement;

    private float playerGroundYPos;
    private float speed;

    private float horizontalGap = 8f;
    private float verticalGap = .7f;
    private float gameOverGap = 3f;
    private Vector3 horizontalDirection;
    private float horizontalDistance;
    private float verticalDistance;

    private bool isGameOver = false;

    void Start()
    {
        // Get PlayerMove and Transform components from GameObject (set to Player)
        playerMovement = playerObject.GetComponent<PlayerMove>();
        playerPosition = playerObject.GetComponent<Transform>();
        playerGroundYPos = playerPosition.position.y;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        // Maintain horizontal distance between Temoc and the player
        horizontalDirection = (playerPosition.position - transform.position).normalized;
        horizontalDirection.y = 0;

        horizontalDistance = Math.Abs(transform.position.x - playerPosition.position.x) + Math.Abs(transform.position.z - playerPosition.position.z);
        if (horizontalDistance > horizontalGap)
        {
            transform.Translate(horizontalDirection * Time.deltaTime * speed);
        }

        // Maintain vertical distance between Temoc and the player
        verticalDistance = transform.position.y - playerPosition.position.y;
        if (verticalDistance > verticalGap + 0.05)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 6);
        }
        if (verticalDistance < verticalGap - 0.05)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 6);
        }

        // Move Temoc when player falls
        GameObject characterModel = GameObject.Find("Player/Ch42_nonPBR@Standard Run").gameObject;
        Animator animator = characterModel.GetComponent<Animator>();
        string current_animation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (current_animation.Equals("Stumble Backwards"))
        {
            isGameOver = true;
            
            StartCoroutine(gameOver());
        }

        // Make Temoc's speed always slightly faster than the player's (so Temoc won't fall behind)
        speed = playerMovement.moveSpeed * 1.3f;
    }

    private IEnumerator gameOver()
    {
        // Move Temoc in front of player
        while (transform.position.z - playerPosition.position.z < gameOverGap)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return new WaitForSeconds(0.001f);
        }

        while (transform.position.y > playerGroundYPos + verticalGap)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            yield return new WaitForSeconds(0.001f);
        }

        yield return null;

        // Turn Temoc around
        int totalRotate = 0;
        int degrees = 15;
        while (totalRotate < 180)
        {
            
            transform.Rotate(0f, degrees, 0f, Space.World);
            totalRotate += degrees;
            yield return new WaitForSeconds(0.01f);
        }

        // Temoc moves toward player
        speed /= 4;
        horizontalDirection = (playerPosition.position - transform.position).normalized;
        horizontalDirection.y = 0;
        horizontalDistance = Math.Abs(transform.position.x - playerPosition.position.x) + Math.Abs(transform.position.z - playerPosition.position.z);
        while (horizontalDistance > 0.1)
        {
            transform.Translate(-horizontalDirection * Time.deltaTime * speed);
            horizontalDirection = (playerPosition.position - transform.position).normalized;
            horizontalDirection.y = 0;
            horizontalDistance = Math.Abs(transform.position.x - playerPosition.position.x) + Math.Abs(transform.position.z - playerPosition.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
