using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    Vector2 Location = new Vector2();
    float R;
     void Start()
     {
        CircleCollider2D body = gameObject.GetComponent<CircleCollider2D>();
        R = body.radius;
     }
     void Update()
     {
        Location.x = gameObject.transform.position.x;
        Location.y = gameObject.transform.position.y;
     }
  


     void OnBecameInvisible()
     {
        if(Location.x + R > ScreenUtils.ScreenRight)
         {
            Location.x = -Location.x -R;
         }
        else if (Location.x - R  < ScreenUtils.ScreenLeft)
        {
            Location.x = -Location.x + R;
        }
        if (Location.y + R > ScreenUtils.ScreenTop)
        {
            Location.y = -Location.y - R;
        }
        else if( Location.y - R < ScreenUtils.ScreenBottom)
        {
            Location.y = -Location.y + R;
        }
        gameObject.transform.position = Location;
     }


}
