using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemyBehavior : MonoBehaviour
{
    public GameObject bullet;
    public GameObject hardness2Bullet;
    public GameObject hardness3Bullet;
    public float waitTime;
    public float bpm;
    public GameObject Target;
    public Transform LeftShootSpot;
    public Transform RightShootSpot;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayMusic()
    {
        counter = 0;
        float looptime = 60 / bpm;
        InvokeRepeating("Shoot", waitTime, looptime);
    }

    void Shoot()
    {
        GameObject newBullet = null;
        counter += 1;

        if (counter == 1)
        {
            newBullet = bullet;
        }
        else if (counter == 2)
        {
            newBullet = bullet;
        }
        else if (counter == 3)
        {
            newBullet = null;
        }
        else if (counter == 4)
        {
            newBullet = hardness2Bullet;
            counter = 0;
        }
        else
        {
            Debug.Log("Should not get here");
            newBullet = null;
        }

        if (newBullet != null)
        {
            BulletBehavior bulletBehavior = Instantiate(newBullet, this.transform).GetComponent<BulletBehavior>();


            float randomValue = Random.value; // Generates a float between 0.0 and 1.0

            if (randomValue < 0.4f)
            {
                // 40% chance
                bulletBehavior.transform.position = this.transform.position;
            }
            else if (randomValue < 0.7f)
            {
                // 30% chance
                bulletBehavior.transform.position = LeftShootSpot.transform.position;
            }
            else
            {
                // 30% chance
                bulletBehavior.transform.position = RightShootSpot.transform.position;
            }

            bulletBehavior.SetUp(Target, 5);
        }
    }
}
