using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrakedItemBehavior : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 10f;
    private int damage;
    public GameObject crakedItem;
    private void Start()
    {
        GameObject player = FindAnyObjectByType<PlayerBehavior>().gameObject;
        SetUp(5);
    }

    public void SetUp(int _damage)
    {
        damage = _damage;
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.one * speed;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehavior>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Weapon"))
        {
            GameObject newCrakedItem = Instantiate(crakedItem);
            newCrakedItem.transform.position = this.transform.position;

            // Get the position of the weapon
            Vector3 weaponPosition = collision.transform.position;

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
            }

            Destroy(this.gameObject);
        }
    }
}
