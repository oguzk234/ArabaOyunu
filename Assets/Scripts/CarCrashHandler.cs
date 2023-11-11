using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashHandler : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private GameObject CrashParticule;
    [SerializeField] private float collisionSpeed;
    [SerializeField] private float crashSpeed;
    [SerializeField]Vector3 DWallCollisionBoxSize = new Vector3(2.0f, 2.0f, 2.0f);

    //SPAWNER
    [SerializeField] WallSpawner wallSpawner;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        collisionSpeed = rg.velocity.magnitude;

        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, DWallCollisionBoxSize);
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject.tag == "DWall")
            {
                collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spawnRoad")
        {
            wallSpawner.spawnRoad(wallSpawner.repeatCount);
            print("roadSPAWNED");
        }


        /*  Toplu method ikisindede ayný TAG var
        if (other.gameObject.tag == "DWall")
        {
            Rigidbody[] rbArray = other.GetComponentsInChildren<Rigidbody>(true);
            foreach(Rigidbody rb in rbArray)
            {
                rb.isKinematic = false;
            }
        }
        */
    }
}
