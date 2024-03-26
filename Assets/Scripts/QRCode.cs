using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QRCode : MonoBehaviour
{

    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        Countdown();
    }

    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            SceneManager.LoadScene("SampleScene");
        }
    }
}
