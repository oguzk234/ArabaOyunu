using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashHandler : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private GameObject CrashParticle;
    [SerializeField] private float carSpeed;
    [SerializeField] private float crashSpeed;
    [SerializeField] Vector3 DestructibleWallCollisionBoxSize = new Vector3(2.0f, 2.0f, 2.0f);
    [SerializeField] private CameraController CameraControlSC;
    [SerializeField] private float destructibleWallShakeCooldown;

    [Header("ShakeShake")]
    [SerializeField] private float wallShakeDuration;
    [SerializeField] private float wallShakeAmount;
    [SerializeField] private float destructibleWallShakeDuration;
    [SerializeField] private float destructibleWallShakeAmount;

    //SPAWNER
    [SerializeField] WallSpawner wallSpawner;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        carSpeed = rg.velocity.magnitude;

        destructibleWallShakeCooldown -= 1 * Time.deltaTime;

        /*
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, DestructibleWallCollisionBoxSize,Quaternion.identity,layerMask:7);
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject.tag == "DWall")
            {
                collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(changePhysicsLayer(collider.gameObject, 0.5f));
                Destroy(collider.gameObject, 9);
            }
        }
        */
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
            if(carSpeed > crashSpeed)
            {
                Vector3 collisionPoint = collision.contacts[0].point;
                Instantiate(CrashParticle, collisionPoint, Quaternion.identity);
                StartCoroutine(CameraControlSC.Shake(wallShakeDuration, crashSpeed * wallShakeAmount));
            }
        }

        if (collision.gameObject.tag == "DWall" && destructibleWallShakeCooldown < 0)
        {
            StartCoroutine(CameraControlSC.Shake(destructibleWallShakeDuration, crashSpeed * destructibleWallShakeAmount));
            destructibleWallShakeCooldown = destructibleWallShakeCooldown = 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spawnRoad")
        {
            wallSpawner.spawnRoad(wallSpawner.repeatCount);
            print("roadSPAWNED");
        }

        if(other.gameObject.GetComponent<DestructibleWall>() != null)
        {
            other.gameObject.GetComponent<DestructibleWall>().goDynamic();
        }
    }
}
