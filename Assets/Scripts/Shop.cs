using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shop : MonoBehaviour
{
    public XRInteractionManager XRInteractionManager;
    public PocketSocket[] PocketSockets;

    [ContextMenu("GetComponents")]
    public void GetComponents()
    {
        XRInteractionManager = FindAnyObjectByType<XRInteractionManager>();
    }

    public void BuyGun(GunManager pGunManager)
    {
        IXRSelectInteractor temp = GetAvailablePocket();
        XRInteractionManager.SelectEnter(temp, Instantiate(pGunManager, temp.transform.position, temp.transform.rotation).GrabInteractable);
    }

    public IXRSelectInteractor GetAvailablePocket()
    {
        foreach (var pocket in PocketSockets)
        {
            if (pocket.interactablesSelected.Count <= 0)
            {
                return pocket;
            }
            
        }
        return null;
    }
}
