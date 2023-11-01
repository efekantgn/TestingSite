using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialMenuInputController : MonoBehaviour
{
    [Header("Inputs")]
    public InputActionProperty cursorMovement;
    public InputActionProperty selection;

    public RadialMenu RadialMenu;

    private void OnEnable()
    {
        cursorMovement.action.performed += CursorMovementPerformed;
        cursorMovement.action.canceled += CursorMovementCanceled;
        selection.action.started += SelectionStarted;
        selection.action.canceled += SelectionCanceled;
    }

    

    private void OnDisable()
    {
        cursorMovement.action.performed -= CursorMovementPerformed;
        cursorMovement.action.canceled -= CursorMovementCanceled;
        selection.action.performed -= SelectionStarted;
        selection.action.canceled -= SelectionCanceled;
    }
    private void SelectionCanceled(InputAction.CallbackContext context)
    {
        RadialMenu.ActivateHighlightedSection();
        RadialMenu.ShowMenu(false);
    }
    private void CursorMovementCanceled(InputAction.CallbackContext context)
    {
        RadialMenu.SetTouchPosition(Vector2.zero);
    }

    private void SelectionStarted(InputAction.CallbackContext context)
    {
        RadialMenu.ShowMenu(true);
    }

    private void CursorMovementPerformed(InputAction.CallbackContext context)
    {
        RadialMenu.WhileCursorMoving(context.ReadValue<Vector2>());
    }

}
