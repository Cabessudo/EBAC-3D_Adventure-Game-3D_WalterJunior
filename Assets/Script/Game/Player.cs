using System.Collections;
using System.Collections.Generic;
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
    private StateBase _playerCurrState;

    private Rigidbody _rb;

    public bool idle;
    public bool walk;
    public bool jump;

    [Header("Movement")]
    public CharacterController player;
    private Vector3 speedVector;
    public float gravity = 9.8f;
    public float speed = 5;
    public float vt;
    public float ht;
    private float gravityForce;

    //Rotation
    public float turnspeed;

    //Jump
    public float jumpForce = 5;

    [Header("Animation")]
    private Animator _anim;

    void Start()
    {
        Init();
        StateMachineInit();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void StateMachineInit()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.Idle, new PlayerState.IdleState());
        stateMachine.RegisterStates(PlayerStates.Walk, new PlayerState.WalkState());
        stateMachine.RegisterStates(PlayerStates.Jump, new PlayerState.JumpState());

        stateMachine.SwitchState(PlayerStates.Idle);
    }

    void Update()
    {
        stateMachine.Update();
        SetCurrentState();

        if(walk && jump && CheckCurrentState(stateMachine.dictionary[PlayerStates.Walk]))
            stateMachine.SwitchState(PlayerStates.Walk, this);

        if(Input.GetKeyDown(KeyCode.Space) && jump && CheckCurrentState(stateMachine.dictionary[PlayerStates.Jump]))
            stateMachine.SwitchState(PlayerStates.Jump, this);

        if(!walk && jump && CheckCurrentState(stateMachine.dictionary[PlayerStates.Idle]))
            stateMachine.SwitchState(PlayerStates.Idle, this);
            
        Checks();
    }

    public void Movement()
    {
        speedVector = transform.forward * vt * speed;

        player.Move(speedVector * Time.deltaTime);    
    }

    void Checks()
    {
        //Check if is walking
        vt = Input.GetAxis("Vertical");
        _anim.SetBool("Run", walk);


        if(vt != 0)
            walk = true;
        else
            walk = false;
            
        //Check if is rotating
        ht = Input.GetAxis("Horizontal");

        //Gravity
        gravityForce -= gravity * Time.deltaTime;        
        speedVector.y = gravityForce;
    }

    public void Rotation()
    {
        transform.Rotate(0, ht * turnspeed * Time.deltaTime, 0);
    }

    public void Jump()
    {
        if(player.isGrounded)
        {
            gravityForce = jumpForce;
        }
    }

    void SetCurrentState()
    {
        _playerCurrState = stateMachine.currState;
    }

    bool CheckCurrentState(StateBase state)
    {
        if(stateMachine.currState != state)
            return true;
        else
            return false;  
    }
}
