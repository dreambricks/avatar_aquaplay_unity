using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cta;
    [SerializeField] private GameObject nationPick;
    [SerializeField] private GameObject points;
    [SerializeField] private GameObject qRCode;

    void Start()
    {
        cta.gameObject.SetActive(true);
        nationPick.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        qRCode.gameObject.SetActive(false);
    }

}
