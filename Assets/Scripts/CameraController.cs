using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform observeable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;
    [SerializeField] Vector3 offset;

    Rigidbody observeableRigidBody;
    void Start()
    {
        observeableRigidBody=observeable.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (observeable == null) return;
        
        Vector3 targetPosition = observeable.position + Vector3.up * cameraHeight + observeableRigidBody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position , targetPosition + offset, followDamping * Time.deltaTime);
    }

}
