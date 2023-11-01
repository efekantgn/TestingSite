using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Allies;

    

    public static AISpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void SpawnCharacters(GameObject[] pChars,int pCount)
    {
        if (pCount >= pChars.Length) pCount = pChars.Length; 

        for (int i = 0; i < pCount; i++)
        {
            pChars[i].SetActive(true);
        }
    }

}
