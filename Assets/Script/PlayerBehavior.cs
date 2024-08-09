using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Slider PlayerHealthSlider;
    private int currentHealth;
    private int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void newGame()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        PlayerHealthSlider.maxValue = maxHealth;
        PlayerHealthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amout)
    {
        currentHealth -= amout;
        PlayerHealthSlider.value = currentHealth;
    }
}
