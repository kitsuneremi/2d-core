using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    private bool levelCompleted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && !levelCompleted)
        {
            Debug.Log("finish level");
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
            

        }
    }


    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelCompleted = false;
    }
}
