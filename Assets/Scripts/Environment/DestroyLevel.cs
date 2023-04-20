using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevel : MonoBehaviour
{
    public string sectionName;

    void Start()
    {
        sectionName = transform.name;
        //StartCoroutine(DestroyClone());
    }
    void OnBecameInvisible()
    {
        StartCoroutine(DestroyClone());
    }

    // Destroy generated sections after a specified time
    IEnumerator DestroyClone()
    {
        //yield return new WaitForSeconds(80);
        yield return new WaitForSeconds(10);

        if (sectionName == "Section(Clone)")
        {
            Destroy(gameObject);
        }
    }

}
