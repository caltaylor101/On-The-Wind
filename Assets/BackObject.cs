using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private float backObjectOffset;
    void Start()
    {
        backObjectOffset = -10.5f;
    }

    // Update is called once per frame
    void Update()
    {
        backObjectOffset -= Time.deltaTime * 1.5f;
        
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + backObjectOffset, 0), Time.deltaTime * 15);
        }
    }
}
