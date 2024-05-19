using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class BossProjectile : ProjectileBase
{
    public Rigidbody shootRb;
    public Transform _player;
    public float minDis = 1;
    
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        ShootBehaviour();
    }

    protected override void Update()
    {
        CheckPlayerPos();
    }

    void ShootBehaviour()
    {
        shootRb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void CheckPlayerPos()
    {
        if(_player != null)
        {
            if(Vector3.Distance(transform.position, _player.position) < minDis)
            {
                var arrowVector = transform.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
                shootRb.AddForce(arrowVector * -1, ForceMode.Force);

            }
        }
    }
}
