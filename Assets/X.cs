using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class X : MonoBehaviour
{
    private BoxCollider2D boxcol;
    private Rigidbody2D rb;
    public bool trigger = false;
    ContactPoint2D[] contacts;



    [SerializeField] private float pushForce = 50f;

    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();

    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            Vector2 collisionDirection = collision.transform.position - transform.position;
            rb = collision.gameObject.GetComponent<Rigidbody2D>();


            if (collisionDirection.x > 0)
            {

                rb.AddForceAtPosition(Vector2.right * 20f, collision.transform.position, ForceMode2D.Impulse);
            }

            else if (collisionDirection.x < 0)
            {
                rb.AddForceAtPosition(Vector2.left * 20f, collision.transform.position, ForceMode2D.Impulse);
            }
            else if (collisionDirection.y > 0)
            {

                rb.AddForceAtPosition(Vector2.up * 10f, collision.transform.position, ForceMode2D.Impulse);
            }
        }

    }*/


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            contacts = collision.contacts;
            if (contacts != null && contacts.Length > 0)
            {
                // Lấy hướng của bề mặt va chạm
                Vector2 surfaceNormal = contacts[0].normal.normalized;

                // Lấy hướng của pedal
                Vector3 pedalAngles = transform.eulerAngles;

                // Tính toán hướng vuông góc với bề mặt
                Vector2 perpendicularDirection = new Vector2(-surfaceNormal.x, -surfaceNormal.y);
                GameObject player = GameObject.Find("Player");
                rb = player.GetComponent<Rigidbody2D>();
                rb.AddForce(perpendicularDirection * pushForce, ForceMode2D.Impulse);
            }
        }

    }
}
