using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MachineGunManager : GunManager
{
    public bool isTriggerActive = false;
    public float FireRate = 1;
    [HideInInspector]public float nextFire;

    protected override void TriggerDeactivated(DeactivateEventArgs arg0)
    {
        if (arg0.interactorObject == GrabInteractable.interactorsSelecting[0])
        {
            isTriggerActive = false;
        }
    }

    protected override void TriggerActivated(ActivateEventArgs arg0)
    {
        
        if (arg0.interactorObject == GrabInteractable.interactorsSelecting[0])
        {
            isTriggerActive = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (isTriggerActive)
        {
            Shoot();
        }
        
    }

    protected override void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            base.Shoot();
        }
    }
}
