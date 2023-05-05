using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNozzleScale : MonoBehaviour
{
    private float speed = 10;
    bool goingUp = false;

    void Awake()
    {
        StartCoroutine(waterMove());
    }

    private IEnumerator waterMove()
    {
        while (true)
        {
            if (transform.localPosition.y <= -8.1f)
            {
                goingUp = true;
                yield return new WaitForSeconds(0.5f);
            }
            if (transform.localPosition.y >= -3.4f)
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

}
