using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class OneHandedGunManager : GunManager
{
    public InputActionProperty LeftHandReleaseMag;
    public InputActionProperty RightHandReleaseMag;


    protected override void OnEnable()
    {
        base.OnEnable();
        GrabInteractable.selectEntered.AddListener(GunSelectEntered);
    }
    

    protected override void OnDisable()
    {
        base.OnDisable();
        GrabInteractable.selectEntered.RemoveListener(GunSelectEntered);


    }

    public override void GunSelectEntered(SelectEnterEventArgs arg0)
    {
        base.GunSelectEntered(arg0);
        ActivateHandButton();
    }
    public void ActivateHandButton()
    {
        if (GrabInteractable.firstInteractorSelecting.transform.CompareTag("RightHand"))
        {
            RightHandReleaseMag.action.started += DisableSocket;
            RightHandReleaseMag.action.canceled += EnableSocket;
            LeftHandReleaseMag.action.started -= DisableSocket;
            LeftHandReleaseMag.action.canceled -= EnableSocket;

        }
        else if (GrabInteractable.firstInteractorSelecting.transform.CompareTag("LeftHand"))
        {
            LeftHandReleaseMag.action.started += DisableSocket;
            LeftHandReleaseMag.action.canceled += EnableSocket;
            RightHandReleaseMag.action.started -= DisableSocket;
            RightHandReleaseMag.action.canceled -= EnableSocket;

        }
        else
        {
            LeftHandReleaseMag.action.started -= DisableSocket;
            LeftHandReleaseMag.action.canceled -= EnableSocket;
            RightHandReleaseMag.action.started -= DisableSocket;
            RightHandReleaseMag.action.canceled -= EnableSocket;
        }


    }

    private void EnableSocket(InputAction.CallbackContext context)
    {
        MagSocket.allowSelect = true;
        
    }

    private void DisableSocket(InputAction.CallbackContext obj)
    {
        MagSocket.allowSelect = false;
    }

    protected override void TriggerActivated(ActivateEventArgs arg0)
    {
        base.TriggerActivated(arg0);
        Shoot();
    }

    
}
