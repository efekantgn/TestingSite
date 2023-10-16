using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem HitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject tempHit = Instantiate(HitEffect.gameObject);
        tempHit.transform.position = collision.contacts[0].point;
        tempHit.GetComponent<ParticleSystem>().Play();
        Destroy(tempHit, HitEffect.main.duration);
        Destroy(gameObject);
        ScoreManager.instance.Score += 10;
        if (collision.transform.gameObject.layer == 7)
            Destroy(collision.transform.gameObject);
    }
    


}
