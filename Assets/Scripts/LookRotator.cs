using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LookRotator : MonoBehaviour
{
    public Transform target = null;
    private XRBaseInteractor interactor = null;

    private void Awake()
    {
        interactor = GetComponent<XRBaseInteractor>();
    }

    private void OnEnable()
    {
       // interactor.onSelectExited.AddListener(ResetRotation);
        interactor.selectExited.AddListener(ResetRotation);
    }

    private void ResetRotation(SelectExitEventArgs arg0)
    {
        transform.localRotation = Quaternion.identity;
    }

    private void OnDisable()
    {
        //interactor.onSelectExited.RemoveListener(ResetRotation);
        interactor.selectExited.RemoveListener(ResetRotation);
    }

    private void ResetRotation(XRBaseInteractable interactable)
    {
        transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (interactor.hasSelection)
            LookAtTarget();
    }

    private void LookAtTarget()
    {
        // Find, and set the direction
        Vector3 direction = target.position - transform.position;
        transform.forward = direction;
    }

    private void OnValidate()
    {
        if(!target)
        {
            // By default, look at the camera
            XROrigin rig = FindObjectOfType<XROrigin>();
            target = rig.Camera.transform;
        }
    }
}
