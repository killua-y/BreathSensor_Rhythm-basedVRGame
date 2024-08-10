using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Slider PlayerHealthSlider;
    public Slider BreathSlider;
    public int currentBreath;
    private int currentHealth;
    private int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        newGame();
    }

    void newGame()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        PlayerHealthSlider.maxValue = maxHealth;
        PlayerHealthSlider.value = currentHealth;

        if (BreathSlider != null)
        {
            BreathSlider.maxValue = 100;
            currentBreath = 50;
            BreathSlider.value = currentBreath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amout)
    {
        currentHealth -= amout;
        PlayerHealthSlider.value = currentHealth;
        Debug.Log("Take damage :" + amout);
    }
}
