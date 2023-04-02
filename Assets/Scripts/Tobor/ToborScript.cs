using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToborScript : MonoBehaviour
{
    private bool right;
    public float toborSpeed = 70;
    // Start is called before the first frame update
    void Start()
    {
        if ((this.gameObject.transform.position.x - LevelBoundary.leftSide) < (LevelBoundary.rightSide - this.gameObject.transform.position.x ))
        {
            right = true;
        }
        else
            right = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(right){
            transform.Translate(Vector3.right * Time.deltaTime * toborSpeed);
        }
        else{
            transform.Translate(Vector3.left * Time.deltaTime * toborSpeed);
        }
        //if((this.gameObject.transform.position.x < LevelBoundary.leftSide) || (this.gameObject.transform.position.x > LevelBoundary.rightSide)){
        //    Destroy(this);
        //}
    }
}
