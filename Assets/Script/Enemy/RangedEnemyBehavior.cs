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
        BulletBehavior bulletBehavior = Instantiate(bullet, this.transform).GetComponent<BulletBehavior>();
        bulletBehavior.transform.position = this.transform.position;
        switch (bpm)
        {
            case 34:
                bulletBehavior.transform.position += new Vector3(0, 2, 0);
                bulletBehavior.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                bulletBehavior.transform.localScale = Vector3.one * 3;
                break;
            case 68:
                bulletBehavior.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                bulletBehavior.transform.localScale = Vector3.one * 2;
                //if (counter == 0)
                //{
                //    Destroy(bulletBehavior);
                //    counter = 1;
                //}
                //else
                //{
                //    counter -= 1;
                //}
                break;
            case 136:
                bulletBehavior.transform.position += new Vector3(0, -2, 0);
                bulletBehavior.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                //if (counter == 0)
                //{
                //    Destroy(bulletBehavior);
                //    counter = 3;
                //}
                //else
                //{
                //    counter -= 1;
                //}
                break;
        }
        bulletBehavior.SetUp(player, 5);
    }
}
