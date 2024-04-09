using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using Anim;

public class MyPlayer : Singleton<MyPlayer>
{
    [Header("Movement")]
    private Rigidbody _rb;
    public Transform orientation;
    public float speed = 10;
    public float dragForce = 5;
    public bool walk; 
    private float _vt;
    private float _ht;

    //Jump
    public GroundCheck groundCheck;
    private KeyCode jumpKey = KeyCode.Space;
    public bool isJumping;
    
    public float jumpForce = 15;

    [Header("Animation")]
    public AnimationBase anim;


    [Header("Life & Death")]
    public Transform spawnPos;
    public HealthPlayer health;
    public List<FlashColor> flashColors;
    public bool isAlive;
    
    //Camera Shake On Damage
    private float camShakeMagnitude = 1;

    [Header("Cloths & PowerUp's")]
    [SerializeField] private ClothChanger _cloths;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SetHealthActions();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            Checks();
            Movement();
            Jump();
        }
    }

    public void Movement()
    {
        Vector3 movementDir = orientation.forward * _vt + orientation.right * _ht;

        _rb.AddForce(movementDir.normalized * speed, ForceMode.Force);
    }

    public void Jump()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim?.SetAnimByType(AnimType.JUMP);
        }
    }

    #region  Checks
    void Checks()
    {
        MovementCheck();   
        GroundCheck();
        FallFromTheWorldCheck();
    }


    void GroundCheck()
    {
        if(groundCheck.grounded)
        {
            _rb.drag = dragForce;

            //Animation Landing
            if(isJumping)
                isJumping = false;
        }
        else
        {
            _rb.drag = dragForce/2;

            //Animation Falling
            if(!isJumping)
            {
                Debug.Log("Falling");
                isJumping = true;
                anim?.SetAnimByType(AnimType.FALL);
            }
        }
    }

    void MovementCheck()
    {
        //Check if is walking
        _vt = Input.GetAxis("Vertical");
        _ht = Input.GetAxis("Horizontal");

        anim?.SetAnimByType(AnimType.RUN, walk);

        if(_vt != 0 || _ht != 0)
            walk = true;
        else if(_vt == 0 && _ht == 0)
            walk = false;
    }

    void FallFromTheWorldCheck()
    {
        if(transform.position.y < -40)
        {
            Revive();
        }
    }
    #endregion

    #region Life & Death
    public void Revive()
    {
        Respawn();
        SetHealthActions();
        anim?.SetAnimByType(AnimType.IDLE);
    }

    void Respawn()
    {
        if(CheckpointManager.Instance.CheckpointsCheck())
            transform.position = CheckpointManager.Instance.GetLastCheckpointPos();
        else
            transform.position = spawnPos.position;
    }

    void SetHealthActions()
    {
        isAlive = true;
        
        if(health != null)
        {
            health.onDamage += OnDamagePlayer;
            health.onKill += OnKillPlayer;
            health.canHit = true;
        }
    }

    void OnDamagePlayer(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManagers.Instance.ChangeVignete();
        ShakeCamera.Instance.Shake(camShakeMagnitude, camShakeMagnitude);
    }

    void OnKillPlayer()
    {
        health.onDamage -= OnDamagePlayer;
        health.onKill -= OnKillPlayer;

        health.canHit = false;
        anim?.SetAnimByType(AnimType.DEATH);
        isAlive = false;
        Invoke(nameof(Revive), 1);
    }
    #endregion

    #region Cloths & PowerUp's
    public void ChangePwupCloth(Texture tex, float time)
    {
        StartCoroutine(ChangeClothRoutine(tex, time));
    }

    public void ChangeCloth(Texture tex)
    {
        _cloths.ChangeCloth(tex);
    }

    IEnumerator ChangeClothRoutine(Texture currTex, float time)
    {
        _cloths.ChangeCloth(currTex);
        yield return new WaitForSeconds(time);
        _cloths.ResetCloth(); 
    }

    public void ChangeSpeed(float speed, float time)
    {
        StartCoroutine(ChangeSpeedRoutine(speed, time));
    }

    IEnumerator ChangeSpeedRoutine(float currSpeed, float time)
    {
        var defaultSpeed = speed;
        speed = currSpeed;
        yield return new WaitForSeconds(time);
        speed = defaultSpeed;   
    }
    #endregion
}
