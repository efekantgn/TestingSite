using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Vitals;

public class Player : MonoBehaviour
{
    [ReadOnlyInspector] public TextMeshPro HealthHud;
    [ReadOnlyInspector] public Health Health;
    [ReadOnlyInspector] public ActionBasedContinuousMoveProvider MoveProvider;
    [ReadOnlyInspector] public CharacterController CharacterController;


    [ContextMenu("GetComponents")]
    public void GetComponents()
    {
        Health = GetComponent<Health>();
        HealthHud = GameObject.Find("HealthHud").GetComponent<TextMeshPro>();
        CharacterController = GetComponent<CharacterController>();
        MoveProvider = FindAnyObjectByType<ActionBasedContinuousMoveProvider>();
    }
    

    public void DecreaseHealth(float amount)
    {
        Health.Decrease(amount);
        HealthHud.text = Health.Value.ToString();
        if (Health.Value <= 0)
        {
            CharacterController.enabled = false;
            MoveProvider.moveSpeed = 0;
            Invoke(nameof(GoToMainMenu), 1);
        }
    }
    public void GoToMainMenu()
    {
        SceneTransitionManager.singleton.GoToScene(0);
    }
}
