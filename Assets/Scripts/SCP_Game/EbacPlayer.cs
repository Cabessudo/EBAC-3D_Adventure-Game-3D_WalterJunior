using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using Cloth;

public class EbacPlayer : Singleton<MyPlayer>
{
    public Rigidbody rb;
    public Transform spawnPos;
    public CharacterController player;
    public Animator anim;
    public HealthPlayer playerHealth;
    [SerializeField] private ClothChanger _cloths;
    public float speed;
    public float runSpeed = 2;
    public float turnSpeed;
    public float gravity = 9.8f;
    private float gravityForce;
    public float jumpForce;
    public bool _isAlive = true;
    private bool _isjumping;

    public KeyCode runKey = KeyCode.LeftShift;
    public List<FlashColor> flashColors;

    [Header("Cam Shake")]
    public float shakeMagnitude = 1;

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
    }

    void Start()
    {
        transform.position = spawnPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        FallFromTheWorldCheck();

        //Rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        //Movement
        var vt = Input.GetAxis("Vertical");
        var speedVector = transform.forward * speed * vt;


        if(player.isGrounded)
        {
            if(_isjumping)
            {
                _isjumping = false;
                anim.SetTrigger("Land");
            }

            gravityForce = 0;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gravityForce = jumpForce;
                if(!_isjumping)
                {
                    _isjumping = true;
                    anim.SetTrigger("Jump");
                }
            }
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
    public void Revive()
    {
        playerHealth.RestartLife();
        SetHealthActions();
        Respaw();
        anim.SetTrigger("Revive");
        Invoke(nameof(EnableCollider), .1f);
    }

    void Respaw()
    {
        if(SaveManager.Instance.setup.checkPointPos != Vector3.zero)
        {
            transform.position = SaveManager.Instance.setup.checkPointPos;
        }
        else
            transform.position = spawnPos.position;
    }

    void SetPlayerHealth()
    {
        if(playerHealth == null) playerHealth = GetComponent<HealthPlayer>();
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
        Invoke(nameof(Revive), 1);

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

    #region Checks
    void FallFromTheWorldCheck()
    {
        if(transform.position.y < -40)
        {
            Revive();
        }
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