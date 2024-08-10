using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class RangedEnemyBehavior : MonoBehaviour
{
    public GameObject bullet;
    public float waitTime;
    public float bpm;
    private GameObject player;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerBehavior>().gameObject;
        float looptime = 60 / bpm;
        InvokeRepeating("Shoot", waitTime, looptime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot()
    {
        CrakedItemBehavior bulletBehavior = Instantiate(bullet, this.transform).GetComponent<CrakedItemBehavior>();
        bulletBehavior.transform.position = this.transform.position;
        bulletBehavior.transform.rotation = this.transform.rotation;

        bulletBehavior.SetUp(5);
    }
}
