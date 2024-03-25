using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationPick : MonoBehaviour
{

    [SerializeField] private GameObject airPick;
    [SerializeField] private GameObject firePick;
    [SerializeField] private GameObject waterPick;
    [SerializeField] private GameObject earthPick;

    private void OnEnable()
    {
        airPick.gameObject.SetActive(false);
        firePick.gameObject.SetActive(false);
        waterPick.gameObject.SetActive(false); 
        earthPick.gameObject.SetActive(false);
    }
}
