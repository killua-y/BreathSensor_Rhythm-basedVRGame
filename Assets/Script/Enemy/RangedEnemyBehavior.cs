using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemyBehavior : MonoBehaviour
{
    public GameObject bullet;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerBehavior>().gameObject;
        InvokeRepeating("Shoot", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot()
    {
        BulletBehavior bulletBehavior = Instantiate(bullet, this.transform).GetComponent<BulletBehavior>();
        bulletBehavior.transform.position = this.transform.position;
        bulletBehavior.SetUp(player, 5);
    }
}
