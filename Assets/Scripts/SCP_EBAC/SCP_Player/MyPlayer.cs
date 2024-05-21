using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using Anim;
using Audio;

public class MyPlayer : Singleton<MyPlayer>
{
    [Header("Movement")]
    private Rigidbody _rb;
    public Transform orientation;
    public float defaultSpeed = 25;
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
    public AnimationBase<AnimPlayerType> anim;


    [Header("Life & Death")]
    public Transform spawnPos;
    public HealthPlayer health;
    public List<FlashColor> flashColors;
    public bool isAlive;
    
    //Camera Shake On Damage
    private float camShakeMagnitude = 1;

    [Header("Cloths & PowerUp's")]
    [SerializeField] private ClothChanger _cloths;
    [SerializeField] private Jetpack _jetpack;

    [Header("Player UI")]
    public PlayerUI playerUI;

    [Header("Player Cam")]
    public PlayerCam playerCam;

    [Header("VFX")]
    public ParticleSystem VFX_Dust;

    [Header("SFX")]
    public AudioSource SFX_player;

    [Header("Shoot")]
    public bool shotgun;
    public bool flamethrower;
    public bool canChangeGun = true;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        SetHealthActions();

        if(SaveManager.Instance.setup.jetpackActive)
            ActiveJetpackCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive && Time.timeScale != 0)
        {
            Checks();
            Movement();
            Jump();
        }
        else
        {
            _jetpack.Stop();
        }
    }

    #region Movements
    public void Movement()
    {
        Vector3 movementDir = orientation.forward * _vt + orientation.right * _ht;

        if(movementDir != Vector3.zero)
        {
            _rb.AddForce(movementDir.normalized * speed, ForceMode.Force);
        }
    }

    public void Jump()
    {

        if(Input.GetKeyDown(jumpKey) && isJumping)
        {
            _jetpack.Active();
        }
        
        if(Input.GetKeyUp(jumpKey) && isJumping && _jetpack.isOn && !groundCheck.grounded)
        {
            _jetpack.Stop();
        }

        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim?.SetAnimByType(AnimPlayerType.JUMP);
            SFXManager.Instance.SetAudioByType(Audio.SFXType.PLAYER_JUMP, SFX_player);
        }
    }
    #endregion

    #region  Checks
    void Checks()
    {
        MovementCheck();   
        GroundCheck();
        FallFromTheWorldCheck();
        CheckVelocity();
    }


    void GroundCheck()
    {
        if(groundCheck.grounded)
        {
            _rb.drag = dragForce;
            VFX_Dust?.Play();
            _jetpack?.Land();

            //Animation Landing
            if(isJumping)
            {
                isJumping = false;
                SFXManager.Instance.SetAudioByType(Audio.SFXType.PLAYER_JUMP, SFX_player);
            }

            
        }
        else
        {
            _rb.drag = dragForce/2;
            VFX_Dust?.Stop();

            //Animation Falling
            if(!isJumping)
            {
                isJumping = true;
                anim?.SetAnimByType(AnimPlayerType.FALL);
            }
        }
    }

    void MovementCheck()
    {
        //Check if is walking
        _vt = Input.GetAxis("Vertical");
        _ht = Input.GetAxis("Horizontal");

        anim?.SetAnimByType(AnimPlayerType.RUN, walk);

        if(_vt != 0 || _ht != 0)
            walk = true;
        else if(_vt == 0 && _ht == 0)
            walk = false;
    }

    void CheckVelocity()
    {
        var vel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        if(vel.magnitude > speed)
        {
            var flatVel = speed * vel.normalized;
            _rb.velocity = new Vector3(flatVel.x, _rb.velocity.y, flatVel.z);
        }
    }

    public void ActiveJetpackCheck()
    {
        _jetpack.gameObject.SetActive(true);
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
        health.RestartLife();
        anim?.SetAnimByType(AnimPlayerType.IDLE);
        playerCam?.EnabledAllCams();
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
        playerUI?.PlayerUIAnim(AnimUIType.HIT);
        flashColors.ForEach(i => i.Flash());
        SFXManager.Instance.SetAudioByType(Audio.SFXType.PLAYER_HURT, SFX_player);
        EffectsManagers.Instance.ChangeVignete();
        ShakeCamera.Instance.Shake(camShakeMagnitude, camShakeMagnitude);
    }

    void OnKillPlayer()
    {
        health.onDamage -= OnDamagePlayer;
        health.onKill -= OnKillPlayer;

        playerCam?.DisableAllCams();
        health.canHit = false;
        anim?.SetAnimByType(AnimPlayerType.DEATH);
        isAlive = false;
        Invoke(nameof(Revive), 3);
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
        playerUI.canChangeCloth = true;
        if(playerUI.first)
            playerUI.firstClothFill.SetDuration();
        else if(!playerUI.first)
            playerUI.secondClothFill.SetDuration();
    }

    //PWUPS
    public void ChangeSpeed(float speed, float time)
    {
        StartCoroutine(ChangeSpeedRoutine(speed, time));
    }

    IEnumerator ChangeSpeedRoutine(float currSpeed, float time)
    {
        speed = currSpeed;
        yield return new WaitForSeconds(time);
        speed = defaultSpeed;   
    }

    public void Shotgun(float time)
    {
        StartCoroutine(ShotgunRoutine(time));
    }

    IEnumerator ShotgunRoutine(float time)
    {
        shotgun = true;
        canChangeGun = true;
        yield return new WaitForSeconds(time);
        canChangeGun = true;
        shotgun = false;
    }

    public void Flamethrower(float time)
    {
        StartCoroutine(FlamethrowerRoutine(time));
    }

    IEnumerator FlamethrowerRoutine(float time)
    {
        flamethrower = true;
        canChangeGun = true;
        yield return new WaitForSeconds(time);
        canChangeGun = true;
        flamethrower = false;
    }
    #endregion
}
