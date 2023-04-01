using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public const int TERRAIN_LENGTH = 60;
    public int zPos = TERRAIN_LENGTH;
    public bool generatingSection = false;
    public int sectionNumber;

    void Update()
    {
        if (generatingSection == false)
        {
            generatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        // Generate new section
        sectionNumber = Random.Range(0, sections.Length);
        Instantiate(sections[sectionNumber], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += TERRAIN_LENGTH;

        yield return new WaitForSeconds(4);
        generatingSection = false;
    }
}
