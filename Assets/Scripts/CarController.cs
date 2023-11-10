using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Stats")]
    //TODO CarStatsSO daha iyi bir isimlendirme olabilirmiş CTRL+R x2 ile kolayca isim değiştirebilirsin
    [SerializeField] SOCarStats carStats;

    private float _turnSpeed;
    private float _acceleration;
    private float _health;

    private Quaternion _targetRotation;
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

    }

    private float SetRotationPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //Rotation Control
            if (rotationAngle > -90f && rotationAngle < 0f) rotationAngle = 0f;
            if (rotationAngle > 180f || rotationAngle < -90f) rotationAngle = 180f;

            _targetRotation = Quaternion.Euler(0, rotationAngle, 0);
            return rotationAngle;
        }
        return 90f;
    }

    private void FixedUpdate()
    {
        //SpeedControl

        float rotAngle = SetRotationPoint();

        //Better Lane Changing
        float _speedMultiplier = 2f;
        float speed = _rigidbody.velocity.magnitude;

        //TODO Çok fazla magic number, bu sayıların bir yerde yazması gerekiyor
        //TODO Bunları kısaltmanın en iyi yolu bir matematik formulüne indirgemek
        //TODO _sppedMultiplier = (80 - speed) + 2 gibi
        //TODO Bunun yerine çok özelleştirmek istiyorsanız AnimationCurve kullanın
        //TODO orada 80 speed'de 1.7 180 ve üstünde de 1 verebilirsiniz
        if (speed > 80) _speedMultiplier = 1.7f;
        if (speed > 120) _speedMultiplier = 1.5f;
        if (speed > 180) _speedMultiplier = 1f;

        float rotMultiplier = 1f;
        //TODO Süslü parantezleri aynı satıra yazmasanız güzel olur, okumayı zorlaştırıyor
        if ((rotAngle < 75f && rotAngle > 60f) || (rotAngle > 105f && rotAngle < 120f)) { rotMultiplier = 1.3f; }
        if ((rotAngle < 60f && rotAngle > 0f) || (rotAngle > 120f && rotAngle < 180f)) { rotMultiplier = 1.5f; }

        _speedMultiplier *= rotMultiplier;

        _rigidbody.AddRelativeForce(Vector3.forward * _acceleration * Time.deltaTime * _speedMultiplier);

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _turnSpeed * Time.fixedDeltaTime);

    }
}
