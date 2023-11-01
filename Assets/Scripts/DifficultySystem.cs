using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySystem : MonoBehaviour
{
    public enum Dificulties
    {
        Easy,
        Normal,
        Hard
    }
    public Dificulties curentDificulty;

    public static DifficultySystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.isLoaded && arg0.buildIndex==1)
        {
            
            switch (curentDificulty)
            {
                case Dificulties.Easy:
                    AISpawner.instance.SpawnCharacters(AISpawner.instance.Enemies, 3);
                    AISpawner.instance.SpawnCharacters(AISpawner.instance.Allies, 2);
                    break;
                case Dificulties.Normal:
                    AISpawner.instance.SpawnCharacters(AISpawner.instance.Enemies, 5);
                    AISpawner.instance.SpawnCharacters(AISpawner.instance.Allies, 2);
                    break;
                case Dificulties.Hard:
                    AISpawner.instance.SpawnCharacters(AISpawner.instance.Enemies, 5);
                    break;
            }
        }
    }
}