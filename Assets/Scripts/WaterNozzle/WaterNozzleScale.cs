using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNozzleScale : MonoBehaviour
{
    private Vector3 scaleChange, positionChange;
    public float speed = 10;
    bool goingUp = false;

    void Awake()
    {
        scaleChange = new Vector3(0, -0.01f, 0);
        positionChange = new Vector3(0, -0.005f, 0);

        StartCoroutine(waterMove());
    }

    private IEnumerator waterMove()
    {
        while (true)
        {
            if (transform.localPosition.y <= -7.85f)
            {
                goingUp = true;
                yield return new WaitForSeconds(2);
            }
            if (transform.localPosition.y >= -3.2f)
            {
                goingUp = false;
            }

            if (goingUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }

            yield return null;
        }
    }

    void Update()
    {
        //  this.gameObject.transform.localScale += scaleChange;
        //  this.gameObject.transform.position += positionChange;

        //  // changes scale of water obj to constantly go up and down
        //  // TODO: have it to change scale at random times + slower speeds too?
        ////StartCoroutine(TimedScaling());
        //  if (this.gameObject.transform.localScale.y < 0.1f || this.gameObject.transform.localScale.y > 1.0f)
        //  {
        //      scaleChange = -scaleChange;
        //      positionChange = -positionChange;
        //    //StartCoroutine(TimedScaling());
        //  }

    }
    IEnumerator TimedScaling() 
    {
        yield return new WaitForSeconds(10);
    }

}
