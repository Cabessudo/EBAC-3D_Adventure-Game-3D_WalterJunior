using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 0;
    public int amountDamage = 1;
    public string[] ignoreTag;

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
        transform.localPosition = Vector3.zero;
        Destroy(gameObject, time);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        foreach(var tags in ignoreTag)
        {
            if(collision.gameObject.tag != tags)
            {
                var damageable = collision.gameObject.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    var dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(amountDamage, dir, 0); 
                }
            }
        }

        Destroy(gameObject);
    }
}
    // private GameObject cam;
    // private float range;

    // private LayerMask target;
    

    // void Shoot()
    // {
    //     RaycastHit hit;

    //     if(Physics.Raycast(cam.transform.position, Vector3.forward, out hit, range))
    //     {
    //         var enemy = hit.transform.GetComponent<Player>();
    //         enemy.transform.position += hit.transform.position;
    //     }
    // }
