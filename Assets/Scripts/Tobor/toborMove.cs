using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toborMove : MonoBehaviour
{
    private bool right;
    public float toborSpeed = 70;

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

    void Update()
    {

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
