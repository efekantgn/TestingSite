using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSpawner : MonoBehaviour
{
    public XRGrabInteractable currentMagazine;
    [ReadOnlyInspector] public XRSimpleInteractable XRSimpleInteractable;
    [ReadOnlyInspector] public XRInteractionManager XRInteractionManager;

    [ContextMenu("GetComponents")]
    public void GetComponents()
    {
        XRSimpleInteractable = GetComponent<XRSimpleInteractable>();
        XRInteractionManager = FindAnyObjectByType<XRInteractionManager>();
    }

    private void OnEnable()
    {
        XRSimpleInteractable.selectEntered.AddListener(MagazineSpawnerGrabbed);
    }
    private void OnDisable()
    {
        XRSimpleInteractable.selectEntered.RemoveListener(MagazineSpawnerGrabbed);
    }

    private void MagazineSpawnerGrabbed(SelectEnterEventArgs arg0)
    {
        if (currentMagazine == null) return;
        XRGrabInteractable magazine = Instantiate(currentMagazine, transform.position, transform.rotation);
        XRInteractionManager.SelectEnter(arg0.interactorObject, magazine);
    }
}
