using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            animator.SetTrigger("death");
            rigid.bodyType = RigidbodyType2D.Static;
            
        }
    }


    public void TriggerRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
