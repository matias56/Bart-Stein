using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PressStart : MonoBehaviour
{
    public Canvas can;
    public Canvas can2;
    public Canvas can3;
    public GameObject bart;
    // Start is called before the first frame update
    void Start()
    {
        can.GetComponent<Canvas>().enabled = true;
        can2.GetComponent<Canvas>().enabled = false;
        can3.GetComponent<Canvas>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            can.GetComponent<Canvas>().enabled = false;
            can2.GetComponent<Canvas>().enabled = true;
            bart.SetActive(false);
        }
    }

    public void Options()
    {
        can2.GetComponent<Canvas>().enabled = false;
        can3.GetComponent<Canvas>().enabled = true;
    }

    public void GoBack()
    {
        can2.GetComponent<Canvas>().enabled = true;
        can3.GetComponent<Canvas>().enabled = false;
    }
}
