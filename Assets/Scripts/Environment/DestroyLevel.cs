using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevel : MonoBehaviour
{
    public string sectionName;
    
    const int NUM_INITIAL_SECTIONS = 10;
    int sectionsToDestroy = NUM_INITIAL_SECTIONS;

    void Start()
    {
        sectionName = transform.name;
        StartCoroutine(DestroyClone());
    }

    // Destroy generated sections after a specified time
    IEnumerator DestroyClone()
    {
        yield return new WaitForSeconds(80);

        //if (sectionsToDestroy >= 0)
        //{
        //    yield return new WaitForSeconds(80); 
        //    sectionsToDestroy--;
        //}
        //else
        //{
        //    yield return new WaitForSeconds(75);
        //}

        if (sectionName == "Section(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
