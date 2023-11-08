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
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }    
    }

    private void FixedUpdate()
    {
        float accelaritonInput = _acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.deltaTime;
        _rigidbody.AddRelativeForce(Vector3.forward * accelaritonInput);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);

    }
}
