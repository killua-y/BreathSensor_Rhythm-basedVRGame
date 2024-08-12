using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    // Adjust this value based on how sensitive you want the detection
    public float minPunchSpeed = 0.2f;   // Minimum speed to consider it a punch

    private Vector3 lastPosition;

    private float punchingDuration = 2f;
    private float countDown;

    void Start()
    {
        lastPosition = this.transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = this.transform.position;
        Vector3 velocity = (currentPosition - lastPosition) / Time.deltaTime;

        // Check if the hand moved forward rapidly
        if (velocity.magnitude > minPunchSpeed)
        {
            Debug.Log("Punch detected!");
            countDown = punchingDuration;
        }

        lastPosition = currentPosition;

        if (countDown >= 0)
        {
            countDown -= Time.deltaTime;
        }
    }

    public bool isPunching()
    {
        if (countDown >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
