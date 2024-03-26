using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private GameObject airPointsPanel;
    [SerializeField] private GameObject firePointsPanel;
    [SerializeField] private GameObject waterPointsPanel;
    [SerializeField] private GameObject earthPointsPanel;

    [SerializeField] private Nation nation;
    [SerializeField] private GameObject qrcode;

    public float totalTime;
    private float currentTime;

    private void OnEnable()
    {

        currentTime = totalTime;

        airPointsPanel.SetActive(false);
        firePointsPanel.SetActive(false);
        waterPointsPanel.SetActive(false);
        earthPointsPanel.SetActive(false);

        switch (nation.nationName)
        {
            case "air":
                airPointsPanel.SetActive(true);
                break;
            case "fire":
                firePointsPanel.SetActive(true);
                break;
            case "water":
                waterPointsPanel.SetActive(true);
                break;
            case "earth":
                earthPointsPanel.SetActive(true);
                break;
            default:
                // code block
                break;
        }
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

            qrcode.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}