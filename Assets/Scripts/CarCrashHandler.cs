using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashHandler : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private GameObject CrashParticule;
    [SerializeField] private float collisionSpeed;
    [SerializeField] private float crashSpeed;
    [SerializeField] Vector3 DWallCollisionBoxSize = new Vector3(2.0f, 2.0f, 2.0f);
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

        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, DWallCollisionBoxSize,Quaternion.identity,layerMask:7);
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject.tag == "DWall")
            {
                collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(changePhysicsLayer(collider.gameObject, 0.5f));
                Destroy(collider.gameObject, 9);
            }
        }
    }

    private IEnumerator changePhysicsLayer(GameObject go,float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        go.layer = 6;
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

        if (collision.gameObject.tag == "DWall" && DWallShakeCD < 0)
        {
            StartCoroutine(CamContSc.Shake(0.5f, crashSpeed * 3.2f));
            DWallShakeCD = DWallShakeCD = 0.1f;
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
