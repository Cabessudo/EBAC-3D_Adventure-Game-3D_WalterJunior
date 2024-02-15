using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool walk;
    public bool jump;
    public bool dead;

    public float speed;
    public float runSpeed;

    public float MaxSpeed
    {
        get { return speed * runSpeed;}
    }
}
