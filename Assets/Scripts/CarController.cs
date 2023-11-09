using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header ("Car Stats")]
    [SerializeField] SOCarStats carStats;

    private float _turnSpeed;
    private float _acceleration;
    private float _health;


    Quaternion targetRotation;
    private Rigidbody _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _turnSpeed = carStats.turnSpeed;
        _acceleration = carStats.acceleration;
        _health = carStats.health;
    }

    private void Update()
    {
        SetRotationPoint();
    }

    private void SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if(plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //Rotation Control
            if(rotationAngle > -90f && rotationAngle < 0f) rotationAngle=0f;
            if (rotationAngle > 180f || rotationAngle < -90f) rotationAngle = 180f;

            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }    
    }

    private void FixedUpdate()
    {
        //SpeedControl
        float speed = _rigidbody.velocity.magnitude;
        float speedMultiplier=1;
        if (speed > 60) speedMultiplier = 0.7f;
        if (speed > 120) speedMultiplier = 0.5f;
        if (speed > 160) speedMultiplier = 0.3f;

        _rigidbody.AddRelativeForce(Vector3.forward * _acceleration * Time.deltaTime * speedMultiplier);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);

    }
}
