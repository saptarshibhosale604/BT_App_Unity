using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluetoothCommunication : MonoBehaviour
{
    public Text deviceName;
    public Text dataToSend;
    public Text infoConnected;

    public Text infoCheck;

    private int buttonPressedCounter;
    private bool IsConnected;
    public static string dataRecived = "";
    // Start is called before the first frame update
    void Start()
    {
        IsConnected = false;
        BluetoothService.CreateBluetoothObject();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsConnected)
        {
            infoConnected.text = "Connected " + buttonPressedCounter.ToString();
            try
            {
                string datain = BluetoothService.ReadFromBluetooth();
                if (datain.Length > 1)
                {
                    dataRecived = datain;
                    print(dataRecived);
                }
                
            }
            catch (Exception e)
            {

            }
        }
        else
        {
            infoConnected.text = "No Connection "  + buttonPressedCounter.ToString();
        }
    }

    public void Button_Connect()
    {
        if (!IsConnected)
        {
            print(deviceName.text.ToString());
            IsConnected = BluetoothService.StartBluetoothConnection(deviceName.text.ToString());
            buttonPressedCounter++;
            infoCheck.text = "Connecting";
        }
    }

    public void Button_Send()
    {
        if (IsConnected && (dataToSend.ToString() != "" || dataToSend.ToString() != null))
        {
            BluetoothService.WritetoBluetooth(dataToSend.text.ToString());
        }
    }


    public void Button_Disconnect()
    {
        if (IsConnected)
        {
            BluetoothService.StopBluetoothConnection();
        }
        Application.Quit();
    }
}
