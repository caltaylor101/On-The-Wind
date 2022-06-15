using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandelionSpotlight : MonoBehaviour
{
    public GameObject targetDandelion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetDandelion.transform.position.x, targetDandelion.transform.position.y + .5f, targetDandelion.transform.position.z);
    }
}
