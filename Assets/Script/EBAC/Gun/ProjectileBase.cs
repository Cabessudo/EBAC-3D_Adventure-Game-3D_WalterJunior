using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 0;
    
    void Update()
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

    public void DestroyObj(float time)
    {
        Destroy(gameObject, time);
    }

    public IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(1);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
