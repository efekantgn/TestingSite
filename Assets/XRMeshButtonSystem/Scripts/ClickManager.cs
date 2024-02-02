using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private int _counter = 0;
    [SerializeField] private TextMeshPro _clickText;


    public void IncreaseCounter()
    {
        _counter++;
        _clickText.text = "you clicked " + _counter.ToString() + " times";
    }

    public void DecreaseCounter() 
    {
        _counter--;
        _clickText.text = "you clicked " + _counter.ToString() + " times";
    }
    
    public void ResetCounter()
    {
        _counter = 0;
        _clickText.text = "you clicked " + _counter.ToString() + " times";

    }

}
