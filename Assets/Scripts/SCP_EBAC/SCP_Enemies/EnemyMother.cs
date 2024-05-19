using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyMother : EnemyShoot
{
    public EnemyBase childs;
    public List<Transform> spawnChildsPos;
    public int amountToSpawn;

    void SpawnChilds()
    {
        if(childs != null && amountToSpawn > 0)
        {
            for(int i = 0; i < amountToSpawn; i++)
            {
                Instantiate(childs, spawnChildsPos[i].transform.position, spawnChildsPos[i].transform.rotation);
            }
        }
    }

    protected override void OnEnemyKill()
    {
        SpawnChilds();
        base.OnEnemyKill();
    }
}
