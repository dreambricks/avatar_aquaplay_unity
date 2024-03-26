using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Profiling;


public class UDPReceiver : MonoBehaviour
{
    Thread receiverThread;
    UdpClient client;
    public int port = 5053;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;
    public static LockFreeQueue<string> myQueue;
    // public time timeLastDataReceived;

    public void Start()
    {
        myQueue = new LockFreeQueue<string>();
        receiverThread = new Thread(
            new ThreadStart(ReceiveData));
        receiverThread.IsBackground = true;
        receiverThread.Start();
    }

    // receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);
                myQueue.Enqueue(data);
                // timeLastDataReceived = getCurrentTime();

                if (printToConsole) { print(data); }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // returns the first item in the queue
    public string GetData()
    {
        if (myQueue.Empty()) return "";

        return myQueue.Dequeue();
    }

    // returns the last item in the queue
    // also clears the queue
    public string GetLastestData()
    {
        string result = "";
        string data = "";
        while ((data = GetData()) != "")
        {
            result = data;
        }

        return result;
    }

    // returns the last item in the queue
    // if it is newer than maxAge seconds
    public string GetLastestNewData(float maxAge) 
    {
    //    if (GetCurrentTime() - timeLastDataReceived > maxAge)
    //        return "";
    //
        return GetLastestData();
    }
}
