using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   

    public float time;
    const float Death = 3f;
    public void ApplyForce(Vector2 Way)
    {
        Rigidbody2D RB2D = GetComponent<Rigidbody2D>(); 
        RB2D.AddForce(Way * 10, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;

        if (time >= Death )
        {
            Destroy(gameObject);
        }
    }
}
