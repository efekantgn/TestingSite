using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponData : MonoBehaviour
{
    public Enemy Enemy;
    public Bullet Bullet;
    public float bulletSpeed;
    public Transform BulletInstantiatePos;

    [ContextMenu("Getcomponents")]
    public void GetComponents()
    {
        Enemy=GetComponentInParent<Enemy>();
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(Bullet, BulletInstantiatePos.position, BulletInstantiatePos.rotation);
        Vector3 bulletDirection;
        try
        {
            bulletDirection =  Enemy.TargetPlayer.position-Enemy.transform.position-Enemy._fov.Yoffset/2;
        }
        catch
        {
            bulletDirection = transform.forward;
        }
        bulletDirection = Vector3.Normalize(bulletDirection);
        bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletSpeed);
        
    }

    
}
