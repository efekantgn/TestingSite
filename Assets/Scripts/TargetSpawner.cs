using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public Transform[] CounterPoints;
    public GameObject Target;
    public int LiveTargetNumber=0;

    private void Update()
    {
        SpawnTarget();
    }

    public async void SpawnTarget()
    {
        
        if (LiveTargetNumber >= 1) return;
        Vector3 spawnpos = Vector3.zero;

        spawnpos.x = Random.Range(CounterPoints[0].position.x, CounterPoints[1].position.x);
        spawnpos.y = Random.Range(CounterPoints[0].position.y, CounterPoints[1].position.y);
        spawnpos.z = Random.Range(CounterPoints[0].position.z, CounterPoints[1].position.z);

        GameObject SpawnedTarget = Instantiate(Target, spawnpos, Quaternion.identity);
        LiveTargetNumber++;
        await UniTask.Delay(2000);
        LiveTargetNumber--;
        Destroy(SpawnedTarget);


    }
}
