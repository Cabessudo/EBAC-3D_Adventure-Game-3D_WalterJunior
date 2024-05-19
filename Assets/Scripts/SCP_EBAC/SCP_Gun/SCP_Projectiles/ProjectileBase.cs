using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 0;
    public float amountDamage = 1;
    public string[] ignoreTag;
    public string tagHit = "Enemy";
    private bool once;


    protected virtual void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void DestroyShoot()
    {
        StartCoroutine(DestroyRoutine());
    }

    public IEnumerator DestroyRoutine(float time = 1)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == tagHit && !once)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                var dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0;

                damageable.Damage(amountDamage, dir, 0); 
            }

            once = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tagHit && !once)
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                var dir = other.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0;

                damageable.Damage(amountDamage, dir, 0); 
            }

            once = true;
            Destroy(gameObject);
        }
    }
}

