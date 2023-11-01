using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public DifficultySystem.Dificulties dificulty;

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SetCurentDificulty);
    }

    public void SetCurentDificulty()
    {
        DifficultySystem.instance.curentDificulty = dificulty;
    }
}
