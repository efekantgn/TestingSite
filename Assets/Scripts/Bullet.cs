using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem HitEffect;
    public float DamageAmount=10;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject tempHit = Instantiate(HitEffect.gameObject);
        tempHit.transform.position = collision.contacts[0].point;
        tempHit.GetComponent<ParticleSystem>().Play();
        Destroy(tempHit, HitEffect.main.duration);
        Destroy(gameObject);
        
        
        if (collision.transform.TryGetComponent<Enemy>(out Enemy pEnemy))
        {
            pEnemy.DamageMe(DamageAmount);
        }
        else if (collision.transform.TryGetComponent<Player>(out Player pPlayer))
        {
            pPlayer.DecreaseHealth(DamageAmount);
        }
        

    }
    


}
