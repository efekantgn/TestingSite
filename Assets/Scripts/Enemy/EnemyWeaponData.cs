using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponData : MonoBehaviour
{
    public Bullet Bullet;
    public float bulletSpeed;
    public float FireRate;
    public Transform BulletInstantiatePos;
    private float nextFire;

    public void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            Bullet bullet = Instantiate(Bullet, BulletInstantiatePos.position, BulletInstantiatePos.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(BulletInstantiatePos.forward * bulletSpeed);
        }
    }
}
