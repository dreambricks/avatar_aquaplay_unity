using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections;

public class Points : MonoBehaviour
{
    [SerializeField] private GameObject airPointsPanel;
    [SerializeField] private GameObject firePointsPanel;
    [SerializeField] private GameObject waterPointsPanel;
    [SerializeField] private GameObject earthPointsPanel;

    [SerializeField] private Nation nation;
    [SerializeField] private GameObject qrcode;


    public Queue<int> valueQueue;
    public float totalTime;
    private float currentTime;
    //public string lastSensor;

    //[SerializeField] private ArduinoCommunication arduinoCommunication;

    private float tempoAteProximaAtribuicao;

    //public SerializableDictionary<string, string> nationSensors = new();

    //public SerializableDictionary<string, int> multiplySensor = new();

    // public string selectedNation;

    CultureInfo cultura = new CultureInfo("pt-BR");
    public int points = 0;
    //public int multiplier = 1;


    public TextMeshProUGUI pointText;

    private void Awake()
    {

 
        //nationSensors = SaveManager.LoadFromJsonFile<SerializableDictionary<string, string>>("nation_dic.json");

        //if (nationSensors == null)
        //{
        //    SaveManager.SaveToJsonFile(nationSensors, "nation_dic.json");
        //}


        //multiplySensor = SaveManager.LoadFromJsonFile<SerializableDictionary<string, int>>("multiplier_dic.json");

        //if (multiplySensor == null)
        //{
        //    SaveManager.SaveToJsonFile(multiplySensor, "multiplier_dic.json");
        //}
    }

    private void AtribuirPontos()
    {

        // Decrementa o tempo restante até a próxima atribuição de pontos
        tempoAteProximaAtribuicao -= Time.deltaTime;

        // Se o tempo restante for menor ou igual a zero, atribui os pontos e reinicia o contador
        if (tempoAteProximaAtribuicao <= 0f)
        {
            points += Random.Range(2,8) * 10;
            tempoAteProximaAtribuicao = Random.Range(3, 6); // Reinicia o contador de tempo
        }
    }


    private void OnEnable()
    {
        tempoAteProximaAtribuicao = 4;

        valueQueue = new Queue<int>();
        //arduinoCommunication.SendMessageToArduino("play");


        currentTime = totalTime;

        airPointsPanel.SetActive(false);
        firePointsPanel.SetActive(false);
        waterPointsPanel.SetActive(false);
        earthPointsPanel.SetActive(false);
        
        switch (nation.nationName)
        {
            case "earth":
             //   selectedNation = FindKeyByValue(nationSensors,"earth");
                earthPointsPanel.SetActive(true);
                break;
            case "water":
              //  selectedNation = FindKeyByValue(nationSensors, "water");
                waterPointsPanel.SetActive(true);
                break;
            case "fire":
              //  selectedNation = FindKeyByValue(nationSensors, "fire");
                firePointsPanel.SetActive(true);
                break;
            case "air":
              //  selectedNation = FindKeyByValue(nationSensors, "air");
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
       // SetPoints();
        AtribuirPontos();
        pointText.text = points.ToString("#,0", cultura) + " pontos";
    }



    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            //arduinoCommunication.SendMessageToArduino("stop");

            DataLog dataLog = new();
            dataLog.status = StatusEnum.Jogou.ToString();
            dataLog.score = points.ToString();
            dataLog.nation = nation.nationName;
            LogUtil.SendLogCSV(dataLog);

            qrcode.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }


    //private void SetPoints()
    //{
       // string data = arduinoCommunication.GetLastestData();
       // if (data == "L" || data == "R" || data == "B")
       // if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.L))
       // {
            // Add a random value to the queue
         //   valueQueue.Enqueue(Random.Range(0, 4));
            //Debug.Log(valueQueue);

            // If the queue has between 2 and 4 elements, start the emptying process
            //if (valueQueue.Count >= 2 && valueQueue.Count <= 4)
            //{
            //    StartCoroutine(EmptyQueue());
            //}
        //}

        //string data = arduinoCommunication.GetLastestData();

        /*if (lastSensor != data)
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
        }*/

   // }

    //IEnumerator EmptyQueue()
    //{
    //    // Wait for a random delay between 2 and 5 seconds
    //    yield return new WaitForSeconds(Random.Range(2, 6));

    //    // Empty the queue and add the values to points
    //    while (valueQueue.Count > 0)
    //    {
    //        points += valueQueue.Dequeue() * 10;
    //    }

    //    Debug.Log("Updated points: " + points);
    //}

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