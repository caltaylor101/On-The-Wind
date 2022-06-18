using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Slider staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        //staminaBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //player.GetComponent<PlayerController>().stamina
            float staminaLeft = Mathf.Clamp01(player.GetComponent<PlayerController>().stamina / 100);
            staminaBar.value = staminaLeft;
            //loadingText.text = progress * 100f + "%";
    }
}
