using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ship : MonoBehaviour
{
    Rigidbody2D RB2D;
    Vector2 thrustDirection = new Vector2();
    const float ThrustForce = 5f;
    const float RotateDegreesPerSecond = 50f;
    Vector2 LocationInfo = new Vector2();
    Vector3 rotation = new Vector3();
    float rotationAmount;
    Animator Flames;
    AudioSource AS;
    AudioClip[] Sounds= new AudioClip[2];
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        RB2D = gameObject.GetComponent<Rigidbody2D>();
        rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
        Flames = gameObject.GetComponent<Animator>();

        AS = gameObject.GetComponent<AudioSource>();
        Sounds [0] = (Resources.Load<AudioClip>(@"Sounds\Wooosh1"));
        Sounds [1] =(Resources.Load<AudioClip>(@"Sounds\Wooosh2"));
    }
    
    //gas function
    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") > 0)
        {
            RB2D.AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);

            Flames.SetBool("IsThrusting", true);

            while (i < 1)
            {
                AS.clip = Sounds[(Random.Range(0, 2))];
                AS.Play();
                i++;
            }
        }
        else
        {
            Flames.SetBool("IsThrusting", false);
            AS.Stop();
            i = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        LocationInfo = gameObject.transform.position;
        rotation.z = gameObject.transform.eulerAngles.z;
        float Head = rotation.z;
        Head = Head * Mathf.Deg2Rad;
        thrustDirection.x =- Mathf.Sin(Head);
        thrustDirection.y = Mathf.Cos(Head);

        if (Input.GetAxis("Axis Rotate") > 0)
        {
            transform.Rotate(Vector3.forward, rotationAmount);
        }
        if (Input.GetAxis("Axis Rotate") < 0)
        {
            transform.Rotate(Vector3.forward, -rotationAmount);
        }

        //shoot bullet
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject Bullet = Instantiate(Resources.Load<GameObject>("PreFAps/bullet"), gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Bullet script =  Bullet.GetComponent<Bullet>();
            script.ApplyForce(thrustDirection);   
            Bullet.GetComponent<AudioSource>().Play();
        }
    }
}
