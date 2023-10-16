using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Magazine : MonoBehaviour
{
    public int MaxBulletNumber = 20;
    public int BulletNumber;
    public bool IsEmpty=false;
    private XRGrabInteractable interactable;
    public InteractionLayerMask InPocket;
    public InteractionLayerMask Default;

    private void Start()
    {
        BulletNumber = MaxBulletNumber;
    }
    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(MagazineSelected);
        interactable.selectExited.AddListener(MagazineRemoved);
    }
    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(MagazineSelected);
        interactable.selectExited.RemoveListener(MagazineRemoved);
    }

    private void MagazineRemoved(SelectExitEventArgs arg0)
    {
            interactable.interactionLayers = Default;
    }

    private void MagazineSelected(SelectEnterEventArgs arg0)
    {
        if (arg0.interactorObject.transform.CompareTag("Socket"))
            interactable.interactionLayers = InPocket;
        
    }

    public void DecreaseBullet(int pAmount)
    {
        BulletNumber -= pAmount; 

        if (BulletNumber <= 0)
            IsEmpty = true;
    }
    
}
