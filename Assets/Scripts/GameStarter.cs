using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public TargetSpawner spawner;
    public int GameSecond=10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            spawner.StartGame(GameSecond);
        }
    }


}
