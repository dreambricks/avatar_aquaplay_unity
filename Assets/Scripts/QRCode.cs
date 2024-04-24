using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QRCode : MonoBehaviour
{

    [SerializeField] private Nation nation;
    [SerializeField] private Points points;
    [SerializeField] private GameObject cta;
    [SerializeField] private GameObject qrcode;

    public float totalTime;
    private float currentTime;

    [SerializeField] private Image image;

    private void OnEnable()
    {
        Debug.Log(points.points);
        currentTime = totalTime;
        image.sprite= null;
  
    }

    private void Update()
    {
        Countdown();
        TryConnectApi();
    }

    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            points.points = 0;
            nation.nationName = "";
            cta.gameObject.SetActive(true);
            qrcode.gameObject.SetActive(false);
        }
    }

    void TryConnectApi()
    {
        if (image.sprite == null)
        {
            GetNewQRCode();
        }
    }


    void GetNewQRCode()
    {
        string url = GameManager.GetAPIUrl();
        string fullUrl = url + "/qr/" + nation.nationName + "/" + points.points.ToString();

        WebRequests.GetTexture(fullUrl,
            (string error) => { Debug.Log("Error!\n" + error); },
            (Texture2D texture2D) =>
            {
                Debug.Log("Success getting the QRCode!\n");
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f), 16f);
                image.sprite = sprite;
            });
    }
}
