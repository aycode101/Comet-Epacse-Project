using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using "GenerateLevel.cs";

public class generateTobors : MonoBehaviour
{
    //public 
    public GameObject modelTobor;
    public GameObject eric;
    public List<GameObject> curTobors;
    
    public float objPositionZ;
    public float maxZDistance = 35.0f;
    public float minZDistance = 25.0f;



    // Start is called before the first frame update
    void Start()
    {
        modelTobor.transform.position = new Vector3(0, 0, 10);
        StartCoroutine(GeneratorCheck()); //
        StartCoroutine(DestroyTobor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            AddTobor();
            float wait = (Time.deltaTime * 10) + Random.Range(3.0f, 3.5f);
            yield return new WaitForSeconds(wait);
        }
    }

    void AddTobor() {
        GameObject newTobor = (GameObject)Instantiate(modelTobor);
        
        objPositionZ = Random.Range(minZDistance, maxZDistance);
        newTobor.transform.position = new Vector3(0, 1.6f, eric.transform.position.z + objPositionZ);

        curTobors.Add(newTobor);
    }

    private IEnumerator DestroyTobor()
    {
        yield return new WaitForSeconds(2);
        foreach (var deleteTobor in curTobors){
            if (deleteTobor.transform.position.z < (eric.transform.position.z - 20))
            {
                curTobors.Remove(deleteTobor);
                Destroy(deleteTobor);
            }
        }
    }
}
