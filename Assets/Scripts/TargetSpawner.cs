using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Transform[] CounterPoints;
    public GameObject Target;
    public bool isGameStarted = false;
    GameObject SpawnedTarget=null;

    private void Update()
    {
        if (isGameStarted && SpawnedTarget==null) 
            SpawnTarget();
    }



    public async void StartGame(int pGameSecond)
    {
        ScoreManager.instance.Score = 0;
        isGameStarted = true;
        await UniTask.Delay(pGameSecond*1000);
        isGameStarted = false;

    }

    [ContextMenu("Start")]
    public async void StartGame()
    {
        ScoreManager.instance.Score = 0;
        isGameStarted = true;
        await UniTask.Delay(10000);
        isGameStarted = false;

    }
    public async void SpawnTarget()
    {
        Vector3 spawnpos = Vector3.zero;

        spawnpos.x = Random.Range(CounterPoints[0].position.x, CounterPoints[1].position.x);
        spawnpos.y = Random.Range(CounterPoints[0].position.y, CounterPoints[1].position.y);
        spawnpos.z = Random.Range(CounterPoints[0].position.z, CounterPoints[1].position.z);

        SpawnedTarget = Instantiate(Target, spawnpos, Quaternion.identity);
        await UniTask.Delay(2000);
        Destroy(SpawnedTarget);


    }
}
