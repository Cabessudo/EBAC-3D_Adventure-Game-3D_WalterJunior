using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class EbacPlayer : Singleton<EbacPlayer>
{
    public Rigidbody rb;
    public CharacterController player;
    public Animator anim;
    public HealthBase playerHealth;
    public float speed;
    public float runSpeed = 2;
    public float turnSpeed;
    public float gravity = 9.8f;
    private float gravityForce;
    public float jumpForce;
    public bool _isAlive = true;

    public KeyCode runKey = KeyCode.LeftShift;
    public List<FlashColor> flashColors;

    [Header("Cam Shake")]
    public float shakeMagnitude = 1;

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
    }

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

        if(!_isAlive)
        {
            _isAlive = true;
            Invoke(nameof(Revive), 2);
        }
    }

    void OnValidate()
    {
        Init();
    }

    void Init()
    {
        PlayerFlash();   
        SetPlayerHealth();
        SetHealthActions();
    }

    #region  Life & Death

    [NaughtyAttributes.Button]
    void Revive()
    {
        playerHealth.RestartLife();
        SetHealthActions();
        Respaw();
        anim.SetTrigger("Revive");
        Invoke(nameof(EnableCollider), .1f);
    }

    void Respaw()
    {
        if(CheckpointManager.Instance.CheckpointsCheck())
        {
            transform.position = CheckpointManager.Instance.GetLastCheckpointPos();
        }
    }

    void SetPlayerHealth()
    {
        if(playerHealth == null) playerHealth = GetComponent<HealthBase>();
    }

    void PlayerFlash()
    {
        flashColors = new List<FlashColor>();

        foreach(var flash in transform.GetComponentsInChildren<FlashColor>())
        {
            if(flash != null)
            flashColors.Add(flash);
        }
    }

    void SetHealthActions()
    {
        if(playerHealth != null) 
        {
            playerHealth.onDamage += OnDamagePlayer;
            playerHealth.onKill += OnKillPlayer;
        }

    }

    public void OnKillPlayer()
    {
        playerHealth.onDamage -= OnDamagePlayer;
        playerHealth.onKill -= OnKillPlayer;

        anim.SetTrigger("Death");
        _isAlive = false;

        //Disable Colliders
        foreach(var colliders in transform.GetComponents<Collider>())
            colliders.enabled = false;
    }

    void EnableCollider()
    {
        foreach(var colliders in transform.GetComponents<Collider>())
            colliders.enabled = true;
    }

    public void OnDamagePlayer(HealthBase health)
    {
        flashColors.ForEach(i => i.Flash()); 
        EffectsManagers.Instance.ChangeVignete();
        ShakeCamera.Instance.Shake(shakeMagnitude, shakeMagnitude);
    }

    #endregion
}