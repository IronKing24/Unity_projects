using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Astroid_Spawner : MonoBehaviour
{
    public Vector3 Top = new Vector3(0, ScreenUtils.ScreenTop, 0);
    public Vector3 Bottom = new Vector3(0, ScreenUtils.ScreenBottom, 0);
    public Vector3 Right = new Vector3(ScreenUtils.ScreenRight, 0, 0);
    public Vector3 Left = new Vector3(ScreenUtils.ScreenLeft, 0, 0);

  

   GameObject AstroidPreFap;
   //spawn points
   Vector3[] Spoints = new Vector3[4];
   //spawn directions
   public Direction[] Dpoints = new Direction [4];
   //death sounds
   AudioClip[] Sounds = new AudioClip[2];
   GameObject Exp;
  public AudioSource As;

    void Start()
    {
        AstroidPreFap = Resources.Load<GameObject>("PreFaps/Astroid");

        Sounds[0] = Resources.Load<AudioClip>("Sounds/Oof");
        Sounds[1] = Resources.Load<AudioClip>("Sounds/Noice");

        Exp = Resources.Load<GameObject>("PreFaps/Explotion");

        As = gameObject.GetComponent<AudioSource>();

        Spoints[0] = Top;
        Spoints [1] = Bottom;
        Spoints [2] = Right;
        Spoints [3] = Left;

       
        Dpoints [0] = Direction.Up ;
        Dpoints [1] = Direction.Down ;
        Dpoints [2] = Direction.Left ;
        Dpoints [3] = Direction.Right ;



        ///Spawn four astroids
        int i = 0;
        while (i<4)
        {
            Spawn(Dpoints[i], Spoints[i]);
            i++;
        }
    }

    void Update()
    {
        // keeps astroids at 4 at all timmes

        int X = GameObject.FindGameObjectsWithTag("Astroid").Length;
        if (X<4)
        {
            Spawn(Dpoints[(Random.Range(0, 4))], Spoints[Random.Range(0, 4)]);
        }
    }

    // spawing Astroid
    public GameObject Spawn(Direction Dposition, Vector3 Spoint)
    {
        GameObject Rock = Instantiate(AstroidPreFap) as GameObject;
        Astroid script = Rock.GetComponent<Astroid>();
        script.Initilize (Dposition,Spoint);
        Animator anim = Rock.GetComponent<Animator>();
        anim.SetInteger("Animation_Selector", Random.Range(1, 4));
        return Rock;
    }

    //Spawns 2 small Astroids
    public void Spawn_Small(float Health, Vector2 Loc)
    {
        int i = 0;
        if (Health == 100)
        {
            while (i < 2)
            {
                GameObject Rock = Spawn(Dpoints[Random.Range(0, 5)], Loc);
                Rock.transform.localScale = transform.localScale / 2;
                Astroid S = Rock.GetComponent<Astroid>();
                S.Health = 50;
                Rock.gameObject.tag = ("Rock");
                i++;
            }
        }
    }

    //Destroy Astroid and play the aprpopriate sound
    public void Explotion(GameObject Dead1, GameObject Dead2, int Sound)
    {
        GameObject explotion = Instantiate(Exp, Dead1.transform.position, Quaternion.identity) as GameObject;
        As.clip = Sounds[Sound];
        As.Play();
        Object.Destroy(Dead1);
        Object.Destroy(Dead2);
        Object.Destroy(explotion, 0.5f);
    }
}
