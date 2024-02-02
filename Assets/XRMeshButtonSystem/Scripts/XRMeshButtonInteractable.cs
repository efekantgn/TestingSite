using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class XRMeshButtonInteractable : XRSimpleInteractable
{
   
    private Renderer _renderer;
    [Header("Clicked Event")]
    [SerializeField] private UnityEvent _onClicked;
    [SerializeField] private Color _highlightedColor;
    private Color _defaultColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void HighlightUIObject(bool pValue)
    {
        if (pValue)
        {
            _renderer.material.color = _highlightedColor;
        }
        else
        {
            _renderer.material.color = _defaultColor;
        }
    }

    public UnityEvent GetUnityEvent()
    {
        return _onClicked;
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (args.interactorObject.transform.TryGetComponent(out XRMeshButtonRayInteractor InteractiveUI))
        {
            HighlightUIObject(true);
            InteractiveUI.ListenForTriggerButton(GetUnityEvent());
            
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (args.interactorObject.transform.TryGetComponent(out XRMeshButtonRayInteractor InteractiveUI))
        {
            HighlightUIObject(false);
            InteractiveUI.CancelListenForTriggerButton();

        }

    }

}


