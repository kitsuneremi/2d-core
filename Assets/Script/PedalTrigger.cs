using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PedalTrigger : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D playerRigidbody;
    ContactPoint2D[] contacts;

    [SerializeField] public float jumpForce = 10f;


    Vector2 collisionPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.Equals("Player"))
        {
            contacts = collision.contacts;

            anim.SetTrigger("Trigger");
            Invoke("PushPlayer", .1f);

            Invoke("ResetTrigger", 1f);
        }
    }


    private void ResetTrigger()
    {
        anim.ResetTrigger("Trigger");
    }

    private void PushPlayer()
    {
        if (contacts != null && contacts.Length > 0)
        {
            // Lấy hướng của bề mặt va chạm giữa pedal và player
            Vector2 surfaceNormal = contacts[0].normal.normalized;
            Debug.Log("x=" + surfaceNormal.x + "y=" + surfaceNormal.y);

            // Lấy hướng của pedal
            float pedalRotation = transform.eulerAngles.z;

/*            if (pedalRotation > 180)
            {
                
            }
            else
            {

            }*/

            // đẩy ngược lại với hướng tiếp xúc, chưa phụ thuộc vào hướng của pedal
            Vector2 perpendicularDirection = new Vector2(-surfaceNormal.x, -surfaceNormal.y);


            GameObject player = GameObject.Find("Player");
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            BoxCollider2D collider = player.GetComponent<BoxCollider2D>();
            playerRigidbody.velocity = perpendicularDirection * jumpForce;
        }
    }
}
