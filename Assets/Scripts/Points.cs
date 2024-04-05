using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public string lastSensor;

    [SerializeField] private ArduinoCommunicationReceiver arduinoCommunicationReceiver;

    public SerializableDictionary<string, string> nationSensors = new();

    public SerializableDictionary<string, int> multiplySensor = new();

    public string selectedNation;

    public int points;
    public int multiplier = 1;

    public Text pointText;

    private void Awake()
    {

 
        nationSensors = SaveManager.LoadFromJsonFile<SerializableDictionary<string, string>>("nation_dic.json");

        if (nationSensors == null)
        {
            SaveManager.SaveToJsonFile(nationSensors, "nation_dic.json");
        }


        multiplySensor = SaveManager.LoadFromJsonFile<SerializableDictionary<string, int>>("multiplier_dic.json");

        if (multiplySensor == null)
        {
            SaveManager.SaveToJsonFile(multiplySensor, "multiplier_dic.json");
        }
    }


    private void OnEnable()
    {
        
        currentTime = totalTime;

        airPointsPanel.SetActive(false);
        firePointsPanel.SetActive(false);
        waterPointsPanel.SetActive(false);
        earthPointsPanel.SetActive(false);

        switch (nation.nationName)
        {
            case "earth":
                selectedNation = FindKeyByValue(nationSensors,"earth");
                earthPointsPanel.SetActive(true);
                break;
            case "water":
                selectedNation = FindKeyByValue(nationSensors, "water");
                waterPointsPanel.SetActive(true);
                break;
            case "fire":
                selectedNation = FindKeyByValue(nationSensors, "fire");
                firePointsPanel.SetActive(true);
                break;
            case "air":
                selectedNation = FindKeyByValue(nationSensors, "air");
                airPointsPanel.SetActive(true);
                break;
            default:
                Debug.Log("Nation Error!");
                break;
        }
    }

    private void Update()
    {
        Countdown();
        SetPoints();
        pointText.text = points.ToString() + "pts";
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


    public void SetPoints()
    {
       
        string data = arduinoCommunicationReceiver.GetLastestData();

        if (lastSensor != data)
        {
            if (data == selectedNation)
            {
                points += 50 * multiplier;
                multiplier = 1;
                lastSensor = data;
            }
            else if (nationSensors.ContainsKey(data))
            {
                points += 10 * multiplier;
                multiplier = 1;
                lastSensor = data;
            }

            if (multiplySensor.ContainsKey(data))
            {
                multiplier *= multiplySensor[data];
                lastSensor = data;

            }
        }

    }

    public static string FindKeyByValue(Dictionary<string, string> dictionary, string value)
    {
        foreach (KeyValuePair<string, string> pair in dictionary)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }
        return null; 
    }
}