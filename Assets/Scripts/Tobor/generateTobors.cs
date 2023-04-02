using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateTobors : MonoBehaviour
{
    public ToborScript curTobor;
    public List<ToborScript> currentTobors;
    private float screenWidthInPoints;

    public float objMinDistance = 5.0f;
    public float objMaxDistance = 10.0f;

    public float objMinY  = -1.4f;
    public float objMaxY = 1.4f;

    public float objMinRotation = -45.0f;
    public float objMaxRotation =  45.0f;

    // Start is called before the first frame update
    void Start()
    {
        screenWidthInPoints = 2.0f * Camera.main.orthographicSize * Camera.main.aspect;
        Debug.Log(screenWidthInPoints);
        StartCoroutine(GeneratorCheck()); // 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateTobor();
            float wait = Random.Range(10, 30);
            yield return new WaitForSeconds(wait);
        }
    }

    void AddTobor(float lastObjectX) {
        ToborScript newTobor = Instantiate(curTobor);
        float objPositionX = lastObjectX + Random.Range(objMinDistance, objMaxDistance);
        newTobor.transform.position = new Vector3(objPositionX, 0, 0);

        float rotation = Random.Range(objMaxRotation, objMaxRotation);
        newTobor.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

        currentTobors.Add(newTobor);
    }

    void GenerateTobor() {
        float playerX = transform.position.x;
        float removeObjectX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;

        List<ToborScript> toborsToRemove = new List<ToborScript>();
        foreach (var obj in currentTobors) {
            float objX = obj.transform.position.x;
            farthestObjectX = Mathf.Max(farthestObjectX, objX);
            if (objX < removeObjectX) {
                toborsToRemove.Add(obj);
            }
        }
        foreach (var obj in toborsToRemove) {
            currentTobors.Remove(obj);
            Destroy(obj);
        }
        if (farthestObjectX < addObjectX) {
            AddTobor(farthestObjectX);
        }
    }
}
