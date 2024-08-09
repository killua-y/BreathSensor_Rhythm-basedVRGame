using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 10f;
    private int damage;

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
            Destroy(this.gameObject);
        }
    }
}
