using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToborScript : MonoBehaviour
{
    private bool right;
    public float toborSpeed = 70;
    //private bool move = true;

    // Start is called before the first frame update
    void Start()
    {
        if ((this.gameObject.transform.position.x - LevelBoundary.leftSide) < (LevelBoundary.rightSide - this.gameObject.transform.position.x))
        {
            right = true;
        }
        else
        {
            right = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(!(transform.position.x > LevelBoundary.leftSide) || !(transform.position.x < LevelBoundary.rightSide))
        //{
        //    right = !right;
        //}

        if (transform.position.x < LevelBoundary.leftSide)
        {
            right = true;
        }
        if (transform.position.x > LevelBoundary.rightSide)
        {
            right = false;
        }

        if (right){
            transform.Translate(Vector3.down * Time.deltaTime * toborSpeed);
        }
        else{
            transform.Translate(Vector3.up * Time.deltaTime * toborSpeed);
        }
    }
    
}
