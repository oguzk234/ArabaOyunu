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

    [Header("CameraShakeOffsets")]
    [SerializeField] float xOffsetMin = -1.2f;
    [SerializeField] float xOffsetMax = 1.54f;
    [SerializeField] float yOffsetMin = -1.6f;
    [SerializeField] float yOffsetMax = 1.6f;


    [SerializeField] Vector3 ShakeAmount;

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


    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = Random.Range(xOffsetMin, xOffsetMax) * magnitude;
            float z = Random.Range(yOffsetMin, yOffsetMax) * magnitude;

            ShakeAmount = new Vector3(x, 0 ,z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        ShakeAmount = Vector3.zero;
    }

}
