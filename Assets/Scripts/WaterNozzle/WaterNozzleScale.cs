using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNozzleScale : MonoBehaviour
{
    private Vector3 scaleChange, positionChange;
    public float speed = 1;

    // Start is called before the first frame update
    void Awake()
    {
        scaleChange = new Vector3(0, -0.01f, 0);
        positionChange = new Vector3(0, -0.005f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale += scaleChange;
        this.gameObject.transform.position += positionChange;

        // changes scale of water obj to constantly go up and down
        // TODO: have it to change scale at random times + slower speeds too?
        if (this.gameObject.transform.localScale.y < 0.1f || this.gameObject.transform.localScale.y > 1.0f)
        {
            scaleChange = -scaleChange;
            positionChange = -positionChange;
            //WaitForSeconds(3);
        }
    }
}
