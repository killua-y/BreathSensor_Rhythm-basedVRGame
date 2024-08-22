using System;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;  // Make sure you add this for UI controls

public class BreathManager : MonoBehaviour
{
    public string portName = "COM3";  // Adjust this to match your Arduino's port
    public int baudRate = 9600;

    private SerialPort serialPort;

    public Slider BreathSlider;  // Reference to the UI Slider for chest_ADC_min

    private void Start()
    {
        // Initialize the serial port
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 100;  // Set a timeout to avoid hanging the program if no data is available
        serialPort.Open();  // Open the serial port
    }

    private void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                // Read data from the serial port
                string data = serialPort.ReadLine();
                Debug.Log("Received: " + data);

                // Parse the data, expecting a format like "170,240"
                string[] values = data.Split(',');

                if (values.Length >= 2)
                {
                    int result = int.Parse(values[1]) - 150;
                    // Set the slider values
                    BreathSlider.value = result;

                    // Optionally, you can update the text next to the sliders if needed
                    Debug.Log("chest: " + int.Parse(values[1]));
                }
            }
            catch (TimeoutException)
            {
                // Handle timeout when no data is available
            }
        }
    }

    private void OnApplicationQuit()
    {
        // Close the serial port when the application quits
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
