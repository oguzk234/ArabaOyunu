using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarController : MonoBehaviour
{
    public Text timerText;
    public Text pointText;
    [Header("Car Stats")]
    //[SerializeField] SOCarStats carStats;

    [SerializeField] GameObject carModel;

    //[SerializeField] private float _speedMultiplier = 2f;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    //[SerializeField] private float AMKYATAYGECISHIZIKATSAYISI;

    //private float _turnSpeed;
    //private float _acceleration;
    //private float _health;

    Quaternion targetRotation;
    private Rigidbody _rigidbody;
    private float _time = 0f;
    private float _point = 0f;

    public float mouseY;
    public float mouseX;

    public bool lose0;
    public bool lose1;
    public float lose2Cd = 0;
    public float lose2CdLOSE = 2;
    public GameObject LOSEPREFAB;


    IEnumerator lose0GO()
    {
        yield return new WaitForSeconds(2);
        lose0 = true;
    }
    private void Awake()
    {
        Time.timeScale = 1;
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(nameof(lose0GO));
        //_turnSpeed = carStats.turnSpeed;
        //_acceleration = carStats.acceleration;
       // _health = carStats.health;

        //print(Mathf.Log(16,2));
        //print(Mathf.Log10(100)); 10döndürür
        /*
        print(Mathf.Log10(5));
        print(Mathf.Log10(10));
        print(Mathf.Log10(15));
        print(Mathf.Log10(20));
        print(Mathf.Log10(25));
        print(Mathf.Log10(30));
        print(Mathf.Log10(35));
        print(Mathf.Log10(40));
        print(Mathf.Log10(45));
        print(Mathf.Log10(50));
        */
    }


    private void FixedUpdate()
    {
        mouseY = Input.mousePosition.y;
        mouseX = Input.mousePosition.x;

        mouseY = (mouseY - Screen.height + Screen.height / 2) / Screen.height;
        mouseX = (mouseX - Screen.width + Screen.width / 2) / Screen.width;
        mouseX += 0.25f;
        mouseX = Mathf.Clamp(mouseX, 0.2f, 0.5f);
        print(mouseX);

        speed = _rigidbody.velocity.magnitude;

        //Quaternion TurnRot = Quaternion.Euler(0,90 + -mouseY*150,0);
        //_rigidbody.rotation = TurnRot;

        /*
        if(_rigidbody.rotation.eulerAngles != new Vector3(0, 90, 0))
        {

            //print("zort");
            float x = _rigidbody.rotation.eulerAngles.y;
            if(x > 90)
            {
                _rigidbody.rotation *= Quaternion.Euler(0, -0.1f, 0);
            }
            if (Mathf.Abs(x - _rigidbody.rotation.eulerAngles.y) <= 0.1f)
            {
                _rigidbody.rotation = Quaternion.Euler(0, 90, 0);
            }

            else if (x < 90)
            {
                _rigidbody.rotation *= Quaternion.Euler(0, 0.1f, 0);
            }
            if(Mathf.Abs(x - _rigidbody.rotation.eulerAngles.y) <= 0.1f)
            {
                _rigidbody.rotation = Quaternion.Euler(0, 90, 0);
            }

            print(x);
            //_rigidbody.rotation *= Quaternion.Euler(0, Mathf.Clamp(-x, -0.4f, 0.4f),0);
        }
        */

        _rigidbody.AddForce(new Vector3(1,0,0) * mouseX *  acceleration * Time.deltaTime + new Vector3(0,0,1) * acceleration*(speed/30)  * mouseY * Time.deltaTime);
        gameObject.transform.rotation = Quaternion.Euler(0, 90 + -_rigidbody.velocity.z/2, 0);


        //_rigidbody.AddRelativeForce(Vector3.forward * velocityProcedure(speed) * _acceleration * Time.deltaTime);

        //_rigidbody.AddRelativeForce(Vector3.forward * _acceleration * Time.deltaTime);


        /*
                 mouseY = Input.mousePosition.y;
        mouseX = Input.mousePosition.x;

        mouseY = (mouseY - Screen.height + Screen.height / 2) / Screen.height;
        mouseX = (mouseX - Screen.width + Screen.width / 2) / Screen.width;

        Vector3 goTo = new Vector3(mouseX, 0, mouseY) * _acceleration * Time.deltaTime;

        _rigidbody.AddForce(goTo);

        gameObject.transform.rotation = Quaternion.Euler(0,90 + -_rigidbody.velocity.z,0);

        */  //YONTEM 1

        //print(mouseX);
        //print(mouseY);

        //Vector3 TargetLoc = gameObject.transform.position + new Vector3(mouseX, 0, mouseY);
        //Vector3 TargetLoc = new Vector3(mouseX, 0, mouseY).normalized;

        //print(TargetLoc);

        //gameObject.transform.LookAt(TargetLoc);


        //_rigidbody.AddRelativeForce(TargetLoc * _acceleration * Time.deltaTime);




        _time += Time.deltaTime;
        timerText.text = "Timer: " + _time.ToString();
        _point = _rigidbody.position.x;
        pointText.text = "Point: " + _point.ToString();




        if(Mathf.Abs(speed) < 9)
        {
            lose1 = true;
        }
        else
        {
            lose1 = false;
        }

        if(lose0 && lose1)
        {
            lose2Cd++;
        }
        else
        {
            lose2Cd = 0;
        }

        if(lose2Cd > lose2CdLOSE)
        {
            Debug.Log("KAYBETTIN");
            Instantiate(LOSEPREFAB).transform.GetChild(0).GetComponent<Text>().text = Mathf.Round(_point).ToString();
            Time.timeScale = 0;
        }

    }

    public void LOSE()
    {

    }

    /*
    public float velocityProcedure(float speed)
    {
        float result = Mathf.Pow(speed, 2) - 4 * speed + 4;
        print(result + "HIZ = " + speed);
        return result;

        //float sonuc = Mathf.Clamp(0.25f * Mathf.Pow(speed, 2),0.001f,500);
        //sonuc /= 100;
        //return sonuc;
    }
    */

    /*
    private void FixedUpdate()
    {
        float mouseY = Input.mousePosition.y;

        Vector3 VerticalDrift = Vector3.forward * Mathf.Clamp((mouseY - Screen.height + Screen.height / 2),-100,100);
        //print(VerticalDrift);

        float rotAngle = SetRotationPoint();

        speed = _rigidbody.velocity.magnitude;

        _speedMultiplier = 1 + (-speed * 0.002f);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _turnSpeed);

        _rigidbody.AddRelativeForce(Vector3.forward * _acceleration * _speedMultiplier * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + (VerticalDrift * _acceleration * 0.00001f * _speedMultiplier* AMKYATAYGECISHIZIKATSAYISI * Time.deltaTime));


        carModel.transform.forward = -_rigidbody.velocity.normalized;

        _time += Time.deltaTime;
        timerText.text = "Timer: " + _time.ToString();



    }
    */
    }
