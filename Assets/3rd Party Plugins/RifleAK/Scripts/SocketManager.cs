using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;
    public GunManager GunManager;
    public AudioSource AudioSource;
    private Transform oldParent;

    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(MagazineSocketted);
        socketInteractor.selectExited.AddListener(MagazineRemovedFromSocket);
    }
    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(MagazineSocketted);
        socketInteractor.selectExited.RemoveListener(MagazineRemovedFromSocket);
    }

    private void MagazineRemovedFromSocket(SelectExitEventArgs arg0)
    {
        GunManager.SocketedMag = null;
        socketInteractor.showInteractableHoverMeshes = true;
        arg0.interactableObject.transform.SetParent(oldParent);

    }

    private void MagazineSocketted(SelectEnterEventArgs arg0)
    {
        AudioSource.Play();
        oldParent = arg0.interactableObject.transform;
        arg0.interactableObject.transform.SetParent(transform);
        GunManager.SocketedMag = arg0.interactableObject.transform.GetComponent<Magazine>();
        socketInteractor.showInteractableHoverMeshes = false;
    }
}
