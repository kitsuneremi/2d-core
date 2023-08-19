using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butlet : MonoBehaviour
{
    [SerializeField] private int lifeTimes = 3;
    public PlayerHp playerHp;
    private void Awake()
    {
        Destroy(gameObject, lifeTimes);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
