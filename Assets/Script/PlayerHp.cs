using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    public int currentHp;
    public HealthBar healthBar;

    private Animator animator;
    private Rigidbody2D rigid;

    private bool death = false;

    void Start()
    {
        currentHp = maxHp;
        healthBar.SetMaxHealth(maxHp);

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(currentHp == 0 && !death)
        {
            death = true;
            animator.SetTrigger("death");
            rigid.bodyType = RigidbodyType2D.Static;
        }
    }

    public void TakeDmg()
    {
        if(currentHp > 0)
        {
            currentHp -= 20;
            healthBar.SetHealth(currentHp);
            
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Toxic"))
        {
            InvokeRepeating(nameof(TakeDmg), 1,1);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Bullet"))
        {
            TakeDmg();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke(nameof(TakeDmg));
    }

} 
