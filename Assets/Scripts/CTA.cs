using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTA : MonoBehaviour
{

    [SerializeField] private GameObject nationPick;
    [SerializeField] private GameObject cta;
    private void OnMouseDown()
    {
        nationPick.gameObject.SetActive(true);
        cta.gameObject.SetActive(false);
    }
}
