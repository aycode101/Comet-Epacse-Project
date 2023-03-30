using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10;
    public float leftRightSpeed = 50;
    public bool isJumping = false;

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
        //if (input.getkey(keycode.w) || input.getkey(keycode.uparrow) || input.getkey(keycode.space))
        //{
        //    if (isjumping == false)
        //    {
        //        isjumping = true;
        //        playerobject.getcomponent<animator>().play("jump");
        //    }
        //}
    }
}
