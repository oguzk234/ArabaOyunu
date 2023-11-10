using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform observeable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] Vector3 offset;


    [SerializeField] Vector3 ShakeAmount;

    Rigidbody observeableRigidBody;
    void Start()
    {
        observeableRigidBody=observeable.GetComponent<Rigidbody>();
        //ShakeAmount = new Vector3(0, 0, 30);

    }

    // Update is called once per frame
    void Update()
    {
        if (observeable == null) return;
        
        targetPosition = observeable.position + Vector3.up * cameraHeight + observeableRigidBody.velocity * aheadSpeed;
        transform.position = Vector3.Lerp(transform.position , targetPosition + offset + ShakeAmount, followDamping * Time.deltaTime);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1.3f, 1.6f) * magnitude;
            float z = Random.Range(-1.6f, 1.6f) * magnitude;

            ShakeAmount = new Vector3(x, 0 ,z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        ShakeAmount = Vector3.zero;
    }
}
