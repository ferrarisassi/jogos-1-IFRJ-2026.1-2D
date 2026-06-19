using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player")){
            Pontucao pontucao = other.GetComponent<Pontucao>();
            pontucao.AddPoints();
            Destroy(this.gameObject);
        }
    }
}
