using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private GameObject CrashParticle;
    [SerializeField] private float carSpeed;
    [SerializeField] private float crashSpeed;
<<<<<<< Updated upstream
    [SerializeField]Vector3 DWallCollisionBoxSize = new Vector3(2.0f, 2.0f, 2.0f);
=======
    [SerializeField] Vector3 DestructibleWallCollisionBoxSize = new Vector3(2.0f, 2.0f, 2.0f);
    [SerializeField] private CameraController CameraControlSC;
    [SerializeField] private float destructibleWallShakeCooldown;

    [Header("ShakeShake")]
    [SerializeField] private float wallShakeDuration;
    [SerializeField] private float wallShakeAmount;
    [SerializeField] private float destructibleWallShakeDuration;
    [SerializeField] private float destructibleWallShakeAmount;
>>>>>>> Stashed changes

    //SPAWNER
    [SerializeField] WallSpawner wallSpawner;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        carSpeed = rg.velocity.magnitude;

<<<<<<< Updated upstream
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, DWallCollisionBoxSize);
=======
        destructibleWallShakeCooldown -= 1 * Time.deltaTime;

        /*
        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, DestructibleWallCollisionBoxSize,Quaternion.identity,layerMask:7);
>>>>>>> Stashed changes
        foreach (Collider collider in colliders)
        {
            if(collider.gameObject.tag == "DWall")
            {
                collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<WallScript>() != null/* || collision.gameObject.GetComponent<>() != null*/)
        {
            if(carSpeed > crashSpeed)
            {
                Vector3 collisionPoint = collision.contacts[0].point;
<<<<<<< Updated upstream
                Instantiate(CrashParticule, collisionPoint, Quaternion.identity);
            }
        }
=======
                Instantiate(CrashParticle, collisionPoint, Quaternion.identity);
                StartCoroutine(CameraControlSC.Shake(wallShakeDuration, crashSpeed * wallShakeAmount));
            }
        }

        if (collision.gameObject.tag == "DWall" && destructibleWallShakeCooldown < 0)
        {
            StartCoroutine(CameraControlSC.Shake(destructibleWallShakeDuration, crashSpeed * destructibleWallShakeAmount));
            destructibleWallShakeCooldown = destructibleWallShakeCooldown = 0.1f;
        }
>>>>>>> Stashed changes
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
