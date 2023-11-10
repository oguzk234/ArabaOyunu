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
        //TODO Yukarıya [RequireComponent(TypeOf(Rigidbody))] ekle
        rg = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        collisionSpeed = rg.velocity.magnitude;

        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, DWallCollisionBoxSize);
        foreach (Collider collider in colliders)
        {
            //TODO Tag kullanmıyoruz
            if(collider.gameObject.tag == "DWall")
            {
                //TODO collider.GetComponent da yapabilirsin
                collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO if(TryGetComponent(out WallScript wallScript))
        if(collision.gameObject.GetComponent<WallScript>() != null/* || collision.gameObject.GetComponent<>() != null*/)
        {
            if(collisionSpeed > crashSpeed)
            {
                //TODO Spawn particle adlı bir methoda taşı burayı
                Vector3 collisionPoint = collision.contacts[0].point;
                Instantiate(CrashParticule, collisionPoint, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO Tag yok, karşıya component koyup get component at
        if (other.gameObject.tag == "spawnRoad")
        {
            //TODO Buradan bir şey spawnlaman doğru değil, eventleri öğrenip event gönder spawnera, buranın tek işlevi crash olması
            //TODO Burada wallSpawner'ı yönetmemeli
            wallSpawner.spawnRoad(wallSpawner.repeatCount);
            print("roadSPAWNED");
        }


        /*  Toplu method ikisindede aynı TAG var
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
