using System.Collections;
using System.Collections.Generic;
using System;
using Anim;
using UnityEngine;
using Ebac.StateMachine;

public class Player : MonoBehaviour
{

    public enum PlayerStates
    {
        Idle,
        Walk,
        Jump
    }

    public StateMachine<PlayerStates> stateMachine;

    private Rigidbody _rb;

    public bool idle;
    public bool walk;
    public bool isJumping;

    [Header("Movement")]
    public Transform orientation;
    public float speed = 5;
    private float _vt;
    private float _ht;

    //Jump
    public float jumpForce = 5;

    [Header("Animation")]
    public AnimationBase anim;
    private Animator _anim;

    [Header("Grounded")]
    public LayerMask whatIsGround;
    public bool grounded;
    public float playerHeight = 1.1f;
    public float dragForce = 300;

    void Start()
    {
        Init();
        StateMachineInit();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _anim = GetComponent<Animator>();
    }

    void StateMachineInit()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.Idle, new PlayerIdleState());
        stateMachine.RegisterStates(PlayerStates.Walk, new PlayerWalkState());
        stateMachine.RegisterStates(PlayerStates.Jump, new PlayerJumpState());

        stateMachine.SwitchState(PlayerStates.Idle);
    }

    void Update()
    {
        Checks(); 
        stateMachine.Update();

        if(walk && CheckCurrentState(stateMachine.dictionary[PlayerStates.Walk]))
            stateMachine.SwitchState(PlayerStates.Walk, this);

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
            Jump();

        if(!walk && !isJumping && CheckCurrentState(stateMachine.dictionary[PlayerStates.Idle]))
            stateMachine.SwitchState(PlayerStates.Idle, this);
            
    }

    public void Movement()
    {
        Vector3 movementDir = orientation.forward * _vt + orientation.right * _ht;

        _rb.AddForce(movementDir.normalized * speed, ForceMode.Force);
    }

    public void Jump()
    {
        isJumping = true;
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetAnimByType(AnimType.JUMP);
    }

    #region  Checks
    void Checks()
    {
        MovementCheck();   
        GroundCheck();
    }

    void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);

        if(grounded)
        {
            _rb.drag = dragForce;

            //Animation Landing
            if(isJumping)
            {
                anim.SetAnimByType(AnimType.LAND);
                isJumping = false;
            }
        }
        else
        {
            _rb.drag = dragForce/2;

            //Animation Falling
            if(!isJumping)
            {
                isJumping = true;
                anim.SetAnimByType(AnimType.FALL);
            }
        }
    }

    void MovementCheck()
    {
        //Check if is walking
        _vt = Input.GetAxis("Vertical");
        _ht = Input.GetAxis("Horizontal");

        _anim.SetBool("Run", walk);

        if(_vt != 0 || _ht != 0)
            walk = true;
        else if(_vt == 0 && _ht == 0)
            walk = false;
    }

    bool CheckCurrentState(StateBase state)
    {
        if(stateMachine.currState != state)
            return true;
        else
            return false;  
    }
    #endregion
}
