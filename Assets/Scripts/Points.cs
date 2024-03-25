using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private GameObject airPointsPanel;
    [SerializeField] private GameObject firePointsPanel;
    [SerializeField] private GameObject waterPointsPanel;
    [SerializeField] private GameObject earthPointsPanel;

    private void OnEnable()
    {
        airPointsPanel.SetActive(false);
        firePointsPanel.SetActive(false);
        waterPointsPanel.SetActive(false);
        earthPointsPanel.SetActive(false);
    }
}
