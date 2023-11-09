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
    private float _speedMultiplier = 1f;


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
        float rotAngle = SetRotationPoint();

        //Better Lane Changing
        float speed = _rigidbody.velocity.magnitude;
        if (speed > 60) _speedMultiplier = 0.8f;
        if (speed > 120) _speedMultiplier = 0.6f;
        if (speed > 160) _speedMultiplier = 0.3f;
        float rotMultiplier=1f;
        if (rotAngle < 15f || rotAngle > 165f) { rotMultiplier = 2f; }
        _speedMultiplier *= rotMultiplier;
    }

    private float SetRotationPoint()
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
            return rotationAngle;
        }
        return 90f;
    }

    private void FixedUpdate()
    {
        //SpeedControl

        _rigidbody.AddRelativeForce(Vector3.forward * _acceleration * Time.deltaTime * _speedMultiplier);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);

    }
}
