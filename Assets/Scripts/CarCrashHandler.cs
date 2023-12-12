using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashHandler : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private GameObject CrashParticule;
    [SerializeField] private float collisionSpeed;
    [SerializeField] private float crashSpeed;
    [SerializeField] private CameraController CamContSc;
    [SerializeField] private float DWallShakeCD;

    //SPAWNER
    [SerializeField] WallSpawner wallSpawner;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        collisionSpeed = rg.velocity.magnitude;

        DWallShakeCD -= 1 * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<WallScript>() != null/* || collision.gameObject.GetComponent<>() != null*/)
        {
            if(collisionSpeed > crashSpeed)
            {
                Vector3 collisionPoint = collision.contacts[0].point;
                Instantiate(CrashParticule, collisionPoint, Quaternion.identity);
                StartCoroutine(CamContSc.Shake(0.7f, crashSpeed * 4));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spawnRoad")
        {
            wallSpawner.spawnRoad();
            print("roadSPAWNED");
        }

        if (other.gameObject.GetComponent<DestructableWall>() != null)
        {
            other.gameObject.GetComponent<DestructableWall>().GoDynamic();
            print("DUVAR DINAMIK OLDU");
        }

        if (other.gameObject.tag == "DWallCrash" && DWallShakeCD < 0)
        {
            StartCoroutine(CamContSc.Shake(0.5f, crashSpeed * 3.2f));
            DWallShakeCD = DWallShakeCD = 0.5f;
            print("SALLANDI");
        }
    }
}
