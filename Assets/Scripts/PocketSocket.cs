using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PocketSocket : XRSocketInteractor
{
    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride => XRBaseInteractable.MovementType.Instantaneous;
}
