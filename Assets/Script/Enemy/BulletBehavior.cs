using Oculus.Interaction;
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
    public GameObject crakedItem;

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
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBehavior>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Weapon"))
        {
            GameObject newCrakedItem = Instantiate(crakedItem);
            newCrakedItem.transform.position = this.transform.position;

            // Get the position of the weapon
            Vector3 weaponPosition = other.transform.position;

            // Apply force to each piece
            foreach (Transform piece in newCrakedItem.transform)
            {
                Rigidbody rb = piece.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Calculate the direction from the weapon to the piece
                    Vector3 forceDirection = piece.position - weaponPosition;
                    forceDirection.Normalize();

                    // Apply force to the piece in the calculated direction
                    float forceMagnitude = 5f; // Adjust this value to control how strong the force is
                    rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
                }

                Destroy(this.gameObject, 3f);
            }

            Destroy(this.gameObject);
        }
    }
}
