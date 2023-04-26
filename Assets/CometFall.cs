using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometFall : MonoBehaviour
{
    public GameObject player;
    public float fallSpeed = 1.5f;
    public AudioSource whoosh;
    bool whooshed = false;
    // Start is called before the first frame update
    void Start()
    {
        whoosh = GameObject.Find("LevelControl/CometSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position.z - this.transform.position.z) <= 20 && this.transform.position.y > 2)
        {
            if(!whooshed){
                whoosh.Play();
                whooshed = true;
            }
            transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * fallSpeed);
        }
    }
}
