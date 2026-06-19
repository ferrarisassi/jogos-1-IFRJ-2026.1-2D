using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameObject coinParticle;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player")){
            Pontucao pontucao = other.GetComponent<Pontucao>();
            pontucao.AddPoints();
            Instantiate(coinParticle, 
                        this.transform.position, 
                        Quaternion.Euler(-90,0,0));
            Destroy(this.gameObject);
        }
    }
}
