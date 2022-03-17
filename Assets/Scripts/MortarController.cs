using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : BaseTower
{
    public override void Shoot()
    {
        Instantiate(bullet, bulletSpawnPositions[0].transform.position, transform.rotation);
        src.Play();

        Instantiate(flash, bulletSpawnPositions[0].transform.position, transform.rotation);

        cools = shootSpeed;
        base.Shoot();
    }
}
