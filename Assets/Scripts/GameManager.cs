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

    public static string apiUrl;


    void Start()
    {
        cta.gameObject.SetActive(true);
        nationPick.gameObject.SetActive(false);
        points.gameObject.SetActive(false);
        qRCode.gameObject.SetActive(false);
    }


    public static string GetAPIUrl()
    {
        apiUrl = "http://localhost:5000";
        string url = apiUrl;
        return url;
    }

}
