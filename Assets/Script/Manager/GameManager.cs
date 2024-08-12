using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public SoundManager soundManager;
    public RangedEnemyBehavior rangedEnemyBehavior;
    public GameObject Leftcontroller;
    public GameObject Rightcontroller;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic()
    {
        Leftcontroller.SetActive(false);
        Rightcontroller.SetActive(false);
        soundManager.PlayMusic();
        rangedEnemyBehavior.PlayMusic();
    }
}
