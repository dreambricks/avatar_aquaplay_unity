using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTA : MonoBehaviour
{

    [SerializeField] private GameObject nationPick;
    private void OnMouseDown()
    {
        nationPick.gameObject.SetActive(true);
        gameObject.gameObject.SetActive(false);
    }
}
