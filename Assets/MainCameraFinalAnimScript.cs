using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFinalAnimScript : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController mainCameraController;

    public GameObject player;
    public GameObject testPlayer;
    //public GameObject background;
    [SerializeField] private Vector3 offset = new Vector3(0, 0f, -2f);

    [SerializeField] int deathPanDistance = 10;
    private int countDeathPanDistance = 0;

    public bool followEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Credits2PCanvas").GetComponent<UI_Credits_Edit>().StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
         if (followEnabled)
        {
            transform.position = player.transform.position + offset;
            transform.rotation = Quaternion.Euler(5, 0, 0);
        }
        //background.transform.position += transform.position;
    }
    public void StartCameraAnimation()
    {
        gameObject.AddComponent<Animator>();
        //gameObject.GetComponent<Animator>().runtimeAnimatorController = 
    }

}
