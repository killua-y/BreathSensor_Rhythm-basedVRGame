using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class BulletBehavior : MonoBehaviour
{
    public int hardness = 1;
    private Rigidbody rb;
    private float speed = 10f;
    private int damage;
    public AudioClip soundClip;

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

    private bool CalculateHardness()
    {
        if (hardness == 1)
        {
            return true;
        }
        else if (hardness == 2) 
        {
            return (BreathManager.isDecreasingBreath || BreathManager.isHoldingBreath);
        }
        else if (hardness == 3)
        {
            return BreathManager.isHoldingBreath;
        }
        else
        {
            return false;
        }
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
            if (!other.GetComponent<WeaponBehavior>().isPunching())
            {
                return;
            }

            if (!CalculateHardness())
            {
                return;
            }

            FindAnyObjectByType<PointTextBehavior>().OnBulletHit(hardness);
            SoundManager.Instance.PlaySound(soundClip);
            EffectManager.Instance.PlayEffect("HitEffect", this.transform.position);

            Destroy(this.gameObject);
        }
    }
}
