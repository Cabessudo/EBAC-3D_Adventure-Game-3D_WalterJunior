using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Shoot Parameters")]
    public float shootSpeed;
    public float timeBtwShoots;

    [Header("Shoot")]
    private Coroutine _currRoutine;
    public List<GameObject> shoots;
    public GameObject shootObj;
    public Transform shootPos;
    public int amountShoot;

    void Start()
    {
        Init();
    }

    void Init()
    {
        shoots = new List<GameObject>();

        for(int i = 0; i < amountShoot; i++)
        {
            var obj = (GameObject)Instantiate(shootObj);
            shoots.Add(obj);
            obj.SetActive(false);
        }
    }

    protected virtual void Shoot()
    {
        var shoot = GetShoot();
        if(shoot != null)
        {
            shoot.SetActive(true);
            shoot.transform.position = shootPos.position;
            shoot.transform.rotation = shootPos.rotation;

            var projectile = shoot.GetComponent<ProjectileBase>();
            if(projectile != null)
            {
                projectile.speed = shootSpeed;
                projectile.DestroyShoot();
            }
        }
    }

    public GameObject GetShoot()
    {
        for(int i = 0; i < shoots.Count; i++)
        {
            if(!shoots[i].activeInHierarchy)
                return shoots[i];
        }

        return null;
    }

    protected virtual IEnumerator ShootRoutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBtwShoots);
        }
    }

    public void StartShoot()
    {
        _currRoutine = StartCoroutine(ShootRoutine());
    }

    public void StopShoot()
    {
        if(_currRoutine != null)
            StopCoroutine(_currRoutine);
    }
}
