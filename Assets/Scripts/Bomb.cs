using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bomb : MonoBehaviour
{
    public int ExplosionForce = 1000;
    private bool isPinExited=true;
    public float explodeTime;
    public XRGrabInteractable Grab;
    public XRSocketInteractor Socket;
    public ParticleSystem ExplosionEffect;
    public float ExplosionRadius = 5f;
    


    private void OnEnable()
    {
        Socket.selectEntered.AddListener(PinPlaced);
        Socket.selectExited.AddListener(PinRemoved);
    }


    private void OnDisable()
    {
        Socket.selectEntered.RemoveListener(PinPlaced);
        Socket.selectExited.RemoveListener(PinRemoved);
    }
    private void Start()
    {
        explodeTime = Mathf.Round(explodeTime * 100f) / 100f;
    }

    private void PinPlaced(SelectEnterEventArgs arg0)
    {
        isPinExited = false;
    }
    private async void PinRemoved(SelectExitEventArgs arg0)
    {
        if (isPinExited) return;
        arg0.interactableObject.transform.SetParent(null);
        isPinExited = true;
        await UniTask.Delay((int)explodeTime*1000);

        Debug.Log("Explode bomb");
        ParticleSystem ps= Instantiate(ExplosionEffect,transform.position,transform.rotation);
        ps.GetComponent<AudioSource>().Play();
        Destroy(ps,ps.main.duration);
        Knockback();
        Destroy(gameObject);
    }

    private void Knockback()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb;
            if (collider.TryGetComponent<Rigidbody>(out rb))
            {
                rb.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
                Debug.Log(rb.transform.name);
            }
        }
    }

    public void Test()
    {
        
    }
}
