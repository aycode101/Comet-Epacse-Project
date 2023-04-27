using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpSpeed = 10;
    public float leftRightSpeed = 50;
    public bool isJumping = false;
    private GameObject characterModel;
    private float jumpVelocity;
    private float playerGroundYPos;

    void Start ()
    {
        playerGroundYPos = this.transform.position.y;
        characterModel = this.transform.Find("Ch42_nonPBR@Standard Run").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        // moving left to right within the boundaries of the SandFloor

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
            }
        }

        jumpVelocity += -30f * Time.deltaTime; //increased the gravity so that the player falls faster
        
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && !isJumping)
        {
            isJumping = true;
            jumpVelocity = jumpSpeed*1.2f;
            characterModel.GetComponent<Animator>().Play("Jump");
        }

        if (isJumping == true)
        {
            transform.Translate(new Vector3(0, jumpVelocity, 0) * Time.deltaTime);
        }

        if (this.transform.position.y <= playerGroundYPos)
        {
            characterModel.GetComponent<Animator>().Play("Standard Run");
            isJumping = false;
        }
    }

}
