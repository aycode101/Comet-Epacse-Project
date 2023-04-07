using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public const int SECTION_LENGTH = 60;
    public int zPos = SECTION_LENGTH;
    public bool generatingSection = false;
    public int sectionNumber;

    int sectionsGenerated = 0;
    const int NUM_INTIAL_SECTIONS = 10;

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

        // generate obstacles here?

        zPos += SECTION_LENGTH;

        if (sectionsGenerated <= NUM_INTIAL_SECTIONS)
        {
            yield return new WaitForSeconds(0.1f);
            sectionsGenerated++;
        } else
        {
            yield return new WaitForSeconds(6);
        }
        generatingSection = false;
    }
}
