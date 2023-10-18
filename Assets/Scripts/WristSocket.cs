using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : XRSocketInteractor
{
    public float targetSize = 0.25f;
    public float sizingDuration = 0.25f;
    
    private Vector3 OriginalScale = Vector3.one;
    private Vector3 objectSize = Vector3.zero;

    private bool canSelect = false;

    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        base.OnHoverEntering(args);
        objectSize=FindObjectSize(args.interactableObject.transform.gameObject);
        interactableHoverScale = FindTargetScale().x;
        if (args.interactableObject.transform.GetComponent<XRGrabInteractable>().isSelected)
            canSelect = true;
    }
    protected override void OnHoverExiting(HoverExitEventArgs args)
    {
        base.OnHoverExiting(args);

        if(!hasSelection)
            canSelect=false;
        objectSize = Vector3.zero;
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        StoreObjectSizeScale(args);
        TweedSizeToSocket(args);
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        showInteractableHoverMeshes = false;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        SetOriginalScale(args);
        showInteractableHoverMeshes = true;
    }

   

    private void TweedSizeToSocket(SelectEnterEventArgs args)
    {
        Vector3 targetScale = FindTargetScale();
        args.interactableObject.transform.localScale = targetScale;
    }

    private Vector3 FindTargetScale()
    {
        float largestSize = FindLargestSize(objectSize);
        float scaleDifference = targetSize / largestSize;
        return Vector3.one * scaleDifference;
    }

    private void SetOriginalScale(SelectExitEventArgs args)
    {
        

        args.interactableObject.transform.localScale = OriginalScale;

        OriginalScale = Vector3.one;
        objectSize = Vector3.zero;
        
    }

    private float FindLargestSize(Vector3 objectSize)
    {
        float largestSize = Mathf.Max(objectSize.x, objectSize.y);
        largestSize = Mathf.Max(largestSize, objectSize.z);
        return largestSize;
    }

    private void StoreObjectSizeScale(SelectEnterEventArgs args)
    {
        objectSize = FindObjectSize(args.interactableObject.transform.gameObject);
        OriginalScale = args.interactableObject.transform.localScale;
    }

    private Vector3 FindObjectSize(GameObject pObjectToMeasure)
    {
        Vector3 size = Vector3.one;

        if (pObjectToMeasure.TryGetComponent(out MeshFilter meshFilter))
        {
            size = ColliderMeasurer.Instance.Measure(meshFilter.mesh);
        }

        return size;
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride => XRBaseInteractable.MovementType.Instantaneous;

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && canSelect;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, targetSize * .5f);
    }
}
