using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    //public GameObject background;
    [SerializeField] private Vector3 offset = new Vector3(0, 2f, -4);

    [SerializeField] int deathPanDistance = 10;
    private int countDeathPanDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {

        if (player.activeSelf)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            CameraDeathPan();
        }
        //background.transform.position += transform.position;
    }

    void CameraDeathPan()
    {
        if (countDeathPanDistance <= deathPanDistance)
        {
            transform.position += new Vector3(.0f, .01f, -.02f);
            transform.eulerAngles += new Vector3(.01f, 0, 0);
            countDeathPanDistance += 1;
        }
    }
}
