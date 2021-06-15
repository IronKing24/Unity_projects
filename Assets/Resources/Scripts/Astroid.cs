using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Astroid : MonoBehaviour
{
    Vector3 cameraveiw;
    Vector3 Clocal;
    Vector3 Location;
    const float MinImpulseForce = 1f;
    const float MaxImpulseForce = 3f;
    Astroid_Spawner Spawner;
    Hud hud;
    AudioClip[] Sounds = new AudioClip[2];
    GameObject Exp;
    AudioSource As;
   
    public float Health = 100;
    public void Start()
    {
        ///Spawn location 
        
        cameraveiw.z = -Camera.main.transform.position.z;
        Clocal = Camera.main.ScreenToWorldPoint(cameraveiw);

        Spawner = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Astroid_Spawner>();
        hud = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Hud>();

        Sounds[0] = Resources.Load<AudioClip>("Sounds/Oof");
        Sounds[1] = Resources.Load<AudioClip>("Sounds/Noice");

        Exp = Resources.Load<GameObject>("PreFaps/Explotion");

        As = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    private void Update()
    {
        Location = transform.position;
    }

    void OnCollisionStay2D(Collision2D CollidedWith)
    {

        if (CollidedWith.gameObject.tag == "Ship")
        {
            Spawner.Spawn_Small(Health, gameObject.transform.position);
            Spawner.Explotion(CollidedWith.gameObject, gameObject, 0);
        }
        else if (CollidedWith.gameObject.tag == "Bullet")
        {
            Spawner.Spawn_Small(Health, gameObject.transform.position);
            Spawner.Explotion(gameObject, CollidedWith.gameObject, 1);
            hud.AddPoint();
        }
    }


    public void Initilize(Direction direction, Vector3 posistion)
    {
        Rigidbody2D Rock2D = gameObject.GetComponent<Rigidbody2D>();
        CircleCollider2D RockC = gameObject.GetComponent<CircleCollider2D>();
        float RockR = RockC.radius;
        ///andgle and fores of the lunch

        float angle = (UnityEngine.Random.Range(0, 30));
        cameraveiw.z = Clocal.z;
        Rock2D.transform.position = new Vector3(posistion.x, posistion.y, Clocal.z);

        switch (direction)
        {
            case Direction.Up:
                {
                    angle += 75;
                    break;
                }
            case Direction.Down:
                {
                    angle += 255;
                    break;
                }
            case Direction.Right:
                {
                    angle -= 15;
                    break;
                }
            case Direction.Left:
                {
                    angle += 165;
                    break;
                }
        }

        if (posistion.y == ScreenUtils.ScreenTop)
        {
            posistion.y = posistion.y + RockR;
        }

        else if (posistion.x == ScreenUtils.ScreenRight)
        {
            posistion.x = posistion.x + RockR;
        }

        else if (posistion.y == ScreenUtils.ScreenBottom)
        {
            posistion.y = posistion.y - RockR;
        }

        else if (posistion.x == ScreenUtils.ScreenLeft)
        {
            posistion.x = posistion.x - RockR;
        }

        //face direction

        if (direction == Direction.Up || direction == Direction.Down)
        {
            transform.Rotate(Vector3.forward, angle + 90);
        }
        else if (direction == Direction.Right || direction == Direction.Left)
        {
            transform.Rotate(Vector3.forward, angle - 90);
        }

            angle = (angle * Mathf.Deg2Rad) * Mathf.PI;
        Vector3 MoveDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = UnityEngine.Random.Range(MinImpulseForce, MaxImpulseForce);

       
        GetComponent<Rigidbody2D>().AddForce(MoveDirection * magnitude, ForceMode2D.Impulse);
        Rock2D.AddForce(MoveDirection, ForceMode2D.Force);
        return ;
    }

    
}
