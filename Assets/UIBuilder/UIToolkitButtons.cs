using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitButtons : MonoBehaviour
{
    public UIDocument document;

    [ContextMenu("GetComponents")]
    public void GetComponents()
    {
        document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        
    }
}
