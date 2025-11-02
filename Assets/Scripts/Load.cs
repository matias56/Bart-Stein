using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Load : MonoBehaviour
{
    public PlayerMovement pM;
    public Gun g;
    public Canvas lo;

    public Slider timerSlider; // Reference to the UI Slider
    public float totalTime = 5f; // Total time in seconds
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        pM.enabled = false;
        g.enabled = false;
        lo.GetComponent<Canvas>().enabled = true;

        timeLeft = totalTime;
        timerSlider.maxValue = totalTime;
        timerSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = totalTime - timeLeft;
        }
        else
        {
            pM.enabled = true;
            g.enabled = true;
            lo.GetComponent<Canvas>().enabled = false;


        }
    }
}
