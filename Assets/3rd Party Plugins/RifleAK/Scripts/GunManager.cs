using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public enum ShootType
{
    Bullet,
    Ray
}

public abstract class GunManager : MonoBehaviour
{
    public Transform BulletInstantiatePos;
    public XRGrabInteractable GrabInteractable;
    public XRSocketInteractor MagSocket;
    public AudioSource AudioSource;
    public AudioClip ShotSFX;

    public GameObject MuzzleFlash;
    public GameObject ShotSFXPrefab;

    public TextMeshPro Hud;
    public Magazine SocketedMag=null;
    public ShootType ShootingType;

    public MagazineSpawner MagazineSpawner;
    public XRGrabInteractable WeaponMagazine;


    [Header("Bullet Settings")]
    public GameObject Bullet;
    public float bulletSpeed;

    [Header("Ray Settings")]
    public LayerMask TargetMask;
    public GameObject HitEffect;

    protected virtual void Start()
    {
        MagazineSpawner = FindAnyObjectByType<MagazineSpawner>();
    }

    protected virtual void OnEnable()
    {

        GrabInteractable.selectEntered.AddListener(GunSelectEntered);
        GrabInteractable.activated.AddListener(TriggerActivated);
        GrabInteractable.deactivated.AddListener(TriggerDeactivated);
    }

    

    public virtual void GunSelectEntered(SelectEnterEventArgs arg0)
    {
        if (MagazineSpawner != null && arg0.interactorObject.transform.TryGetComponent(out XRDirectInteractor component))
            MagazineSpawner.currentMagazine = WeaponMagazine;
    }

    protected virtual void OnDisable()
    {
        GrabInteractable.selectEntered.RemoveListener(GunSelectEntered);
        GrabInteractable.activated.RemoveListener(TriggerActivated);
        GrabInteractable.deactivated.RemoveListener(TriggerDeactivated);
        
    }



    protected virtual void TriggerActivated(ActivateEventArgs arg0)
    {
        
    }

    protected virtual void TriggerDeactivated(DeactivateEventArgs arg0)
    {
        
    }

    protected virtual void Update()
    {
        Hud.text = SocketedMag?.BulletNumber.ToString() ?? "0";
        
    }

    protected virtual void Shoot()
    {

        if (SocketedMag == null || SocketedMag.IsEmpty)
        {
            AudioSource.Play();
            return;
        }


        if (ShootingType==ShootType.Ray)
            RayShoot(); 
        else if(ShootingType == ShootType.Bullet)
            PhysicalBulletShoot();

        
        
        Destroy(Instantiate(MuzzleFlash,BulletInstantiatePos.position,BulletInstantiatePos.rotation),2);
        AudioSource ShotSFX= Instantiate(ShotSFXPrefab, BulletInstantiatePos.position, BulletInstantiatePos.rotation).GetComponent<AudioSource>();
        ShotSFX.clip = this.ShotSFX;
        ShotSFX.Play();
        Destroy(ShotSFX.gameObject,2);
        SocketedMag.DecreaseBullet(1);
    }

    protected void PhysicalBulletShoot()
    {
        
        GameObject bullet = Instantiate(Bullet, BulletInstantiatePos.position, BulletInstantiatePos.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(BulletInstantiatePos.forward * bulletSpeed);
        
    }

    protected void RayShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(BulletInstantiatePos.position, BulletInstantiatePos.forward, out hit, 10, TargetMask))
        {
            GameObject tempHitEffect = Instantiate(HitEffect);
            tempHitEffect.transform.position = hit.point;
            tempHitEffect.GetComponent<ParticleSystem>().Play();
            ScoreManager.instance.Score += 10;
            Destroy(hit.collider.gameObject);
            Destroy(tempHitEffect, tempHitEffect.GetComponent<ParticleSystem>().main.duration);


        }
    }
}
