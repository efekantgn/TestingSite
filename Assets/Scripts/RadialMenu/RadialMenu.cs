using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialMenu : MonoBehaviour
{
    
    [Header("Scene")]
    public Transform selectionTransform = null;
    public Transform cursorTransform = null;

    [Header("Events")]
    public RadialMenuSection top = null;
    public RadialMenuSection right = null;
    public RadialMenuSection bottom = null;
    public RadialMenuSection left = null;

    private Vector2 touchPosition = Vector2.zero;
    private List<RadialMenuSection> radialSections = null;
    private RadialMenuSection highlightedSection = null;

    private readonly float degreeIncrement = 90f;


    private void Awake()
    {
        CreateAndSetupSections();
    }

    private void CreateAndSetupSections()
    {
        radialSections = new List<RadialMenuSection>()
        {
            top, right, bottom, left
        };

        foreach (RadialMenuSection section in radialSections)
            section.iconRenderer.sprite = section.icon;
            
    }

    private void Start()
    {
        ShowMenu(false);
    }

    public void WhileCursorMoving(Vector2 pValue)
    {
        SetTouchPosition(pValue);
        Vector2 dir = Vector2.zero + touchPosition;
        float rot = GetDegree(dir);
        SetSelectionRotation(rot);
        SetSelectedEvent(rot);
    }

    public void SetCursorPosition()
    {
        cursorTransform.localPosition = touchPosition;
    }

    public void SetTouchPosition(Vector2 pValue)
    {
        touchPosition = pValue;
        SetCursorPosition();
    }

    public void ShowMenu(bool pValue)
    {
        gameObject.SetActive(pValue);
    }

    public float GetDegree(Vector2 direction)
    {
        float value = Mathf.Atan2(direction.x, direction.y);
        value *= Mathf.Rad2Deg;

        if (value < 0) value += 360f;
        return value;
    }

    public void SetSelectionRotation(float newRotation)
    {
        float snappedRotation = SnapRotation(newRotation);
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRotation);
    }
    private float SnapRotation(float rotation)
    {
        return GetNearestIncrement(rotation) * degreeIncrement;
    }

    private int GetNearestIncrement(float rotation)
    {
        return Mathf.RoundToInt(rotation / degreeIncrement);
    }
    private void SetSelectedEvent(float currentRotation)
    {
        int index = GetNearestIncrement(currentRotation);

        if (index == 4) index = 0;

        highlightedSection = radialSections[index];
    }

    public void ActivateHighlightedSection()
    {
        highlightedSection.onPress.Invoke();
    }

}
