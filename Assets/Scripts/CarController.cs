using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Text timerText;
    [Header("Car Stats")]
    [SerializeField] SOCarStats carStats;

    [SerializeField] GameObject carModel;

    [SerializeField] private float _speedMultiplier = 2f;
    [SerializeField] private float speed;
    [SerializeField] private float AMKYATAYGECISHIZIKATSAYISI;

    private float _turnSpeed;
    private float _acceleration;
    private float _health;
    
    Quaternion targetRotation;
    private Rigidbody _rigidbody;
    private float time=0f;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _turnSpeed = carStats.turnSpeed;
        _acceleration = carStats.acceleration;
        _health = carStats.health;
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

            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
            return rotationAngle;
        }
        return 90f;
    }

    private void FixedUpdate()
    {
        float mouseY = Input.mousePosition.y;

        Vector3 VerticalDrift = Vector3.forward * Mathf.Clamp((mouseY - Screen.height + Screen.height / 2),-350,350);
        print(VerticalDrift);

        float rotAngle = SetRotationPoint();

        speed = _rigidbody.velocity.magnitude;

        _speedMultiplier = 1 + (-speed * 0.002f);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _turnSpeed);

        _rigidbody.AddRelativeForce(Vector3.forward * _acceleration * _speedMultiplier * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + (VerticalDrift * _acceleration * 0.00001f * _speedMultiplier* AMKYATAYGECISHIZIKATSAYISI * Time.deltaTime));


        carModel.transform.forward = -_rigidbody.velocity.normalized;

        //Todo Daha iyi yapilabilir mi
        //time += Time.deltaTime;
        //timerText.text=time.ToString();

    }
}
