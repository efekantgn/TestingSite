using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    public GameObject PistolGate;
    bool isGateOpened = false;

    public void OpenGate()
    {
        if (!isGateOpened)
        {
            isGateOpened = true;
            PistolGate.transform.DORotate(PistolGate.transform.rotation.eulerAngles+new Vector3(0,-120,0), 1);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet")|| collision.transform.CompareTag("Bomb"))
            OpenGate();
    }
}
