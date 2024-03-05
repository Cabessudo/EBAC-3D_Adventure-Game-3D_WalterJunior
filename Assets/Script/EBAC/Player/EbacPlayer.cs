using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbacPlayer : MonoBehaviour, IDamageable
{
    public Rigidbody rb;
    public CharacterController player;
    public Animator anim;
    public float speed;
    public float runSpeed = 2;
    public float turnSpeed;
    public float gravity = 9.8f;
    private float gravityForce;
    public float jumpForce;
    public int health = 10;

    public KeyCode runKey = KeyCode.LeftShift;
    public List<FlashColor> flashColors;

    // Update is called once per frame
    void Update()
    {
        //Rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        //Movement
        var vt = Input.GetAxis("Vertical");
        var speedVector = transform.forward * speed * vt;


        if(player.isGrounded)
        {
            gravityForce = 0;
            if(Input.GetKeyDown(KeyCode.Space))
                gravityForce = jumpForce;
        }

        gravityForce -= gravity * Time.deltaTime; 
        speedVector.y = gravityForce;

        var isWalking = vt != 0;
        if(isWalking)
        {
            if(Input.GetKey(runKey))
            {
                speedVector *= runSpeed;
                anim.speed = runSpeed;
            }
            else
                anim.speed = 1;
        }

        player.Move(speedVector * Time.deltaTime);
        anim.SetBool("Run", vt != 0);
    }

    void OnValidate()
    {
        Init();
    }

    void Init()
    {
        flashColors = new List<FlashColor>();

        foreach(var flash in transform.GetComponentsInChildren<FlashColor>())
        {
            if(flash != null)
            flashColors.Add(flash);
        }
    }

    public void Damage(int damage)
    {
        flashColors.ForEach(i => i.Flash()); 
        health -= damage;  
    }

    public void Damage(int damage, Vector3 dir, float force = 0)
    {
        Damage(damage);
    }
}
