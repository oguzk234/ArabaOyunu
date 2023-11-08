using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashHandler : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private GameObject CrashParticule;
    [SerializeField] private float collisionSpeed;
    [SerializeField] private float crashSpeed;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        collisionSpeed = rg.velocity.magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<WallScript>() != null/* || collision.gameObject.GetComponent<>() != null*/)
        {
            if(collisionSpeed > crashSpeed)
            {
                Vector3 collisionPoint = collision.contacts[0].point;
                Instantiate(CrashParticule, collisionPoint, Quaternion.identity);
            }
        }
    }
}
