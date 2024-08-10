using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 10f;
    private int damage;
    public GameObject destoryEffect;

    private void Start()
    {
        GameObject player = FindAnyObjectByType<PlayerBehavior>().gameObject;
        SetUp(player, 5);
    }

    public void SetUp(GameObject _target, int _damage)
    {
        damage = _damage;
        rb = this.gameObject.GetComponent<Rigidbody>();
        Vector3 direction = (_target.transform.position - this.transform.position).normalized;
        rb.velocity = direction * speed;
        //rb.AddForce(direction * speed, ForceMode.Impulse);
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerBehavior>().TakeDamage(damage);
        }
        else if (other.CompareTag("Weapon"))
        {
            Instantiate(destoryEffect);
            destoryEffect.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
