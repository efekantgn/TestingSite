using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class XRMeshButtonRayInteractor : XRRayInteractor
{
    
    private UnityEvent _clickEvent;
    private ActionBasedController _actionController;
    private InputAction _triggerAction;


    protected override void Awake()
    {
        base.Awake();
        _actionController = GetComponent<ActionBasedController>();
        _triggerAction = _actionController.activateAction.action;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _triggerAction.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _triggerAction.Disable();
    }

    public void ListenForTriggerButton(UnityEvent unityEvent)
    {
        _triggerAction.started += OnTriggerButtonPressed;
        _clickEvent = unityEvent;

    }

    public void CancelListenForTriggerButton()
    {
        _triggerAction.started -= OnTriggerButtonPressed;
        _clickEvent = null;

    }

    private void OnTriggerButtonPressed(InputAction.CallbackContext obj)
    {
        _clickEvent.Invoke();
    }

 

}
