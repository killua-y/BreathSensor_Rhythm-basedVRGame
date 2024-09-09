using System;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class BreathManager : MonoBehaviour
{
    public string portName = "COM3";  // Adjust this to match your Arduino's port
    public int baudRate = 9600;
    public int max;
    public int min;

    private SerialPort serialPort;

    public Slider BreathSlider;  // Reference to the UI Slider for chest_ADC_min
    public Image BreathSliderFill;  // Reference to the Slider's Fill component

    public static bool isHoldingBreath;
    public static bool isDecreasingBreath;

    public float holdingThreshold = 3.0f;  // Acceptable range for holding breath
    public float holdingTime = 2.0f;  // Time in seconds to consider breath as held

    private int lastBreathValue;
    private float lastUpdateTime;

    private Color normalColor = Color.green;
    private Color holdingColor = Color.yellow;
    private Color decreasingColor = Color.red;

    private void Start()
    {
        // Initialize the serial port
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 100;  // Set a timeout to avoid hanging the program if no data is available
        serialPort.Open();  // Open the serial port
        BreathSlider.maxValue = max - min;

        lastBreathValue = 0;
        lastUpdateTime = Time.time;
        isHoldingBreath = false;
        isDecreasingBreath = false;

        // Set the initial color of the breath slider
        BreathSliderFill.color = normalColor;
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
                    int currentBreathValue = int.Parse(values[1]) - min;
                    // Set the slider values
                    BreathSlider.value = currentBreathValue;

                    // Check if breath is decreasing
                    if (currentBreathValue < lastBreathValue)
                    {
                        isDecreasingBreath = true;
                    }
                    else
                    {
                        isDecreasingBreath = false;
                    }

                    // Check if the breath is being held
                    if (Mathf.Abs(currentBreathValue - lastBreathValue) <= holdingThreshold)
                    {
                        if (Time.time - lastUpdateTime >= holdingTime)
                        {
                            isHoldingBreath = true;
                        }
                    }
                    else
                    {
                        isHoldingBreath = false;
                        lastUpdateTime = Time.time;  // Update the time when breath changes
                    }

                    // Update the last breath value
                    lastBreathValue = currentBreathValue;

                    // Update slider color based on breath states
                    if (isHoldingBreath)
                    {
                        BreathSliderFill.color = holdingColor;  // Yellow when holding breath
                    }
                    else if (isDecreasingBreath)
                    {
                        BreathSliderFill.color = decreasingColor;  // Red when breath is decreasing
                    }
                    else
                    {
                        BreathSliderFill.color = normalColor;  // Green in normal state
                    }

                    // Debug information
                    Debug.Log("Chest: " + int.Parse(values[1]));
                    Debug.Log("isHoldingBreath: " + isHoldingBreath);
                    Debug.Log("isDecreasingBreath: " + isDecreasingBreath);
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
