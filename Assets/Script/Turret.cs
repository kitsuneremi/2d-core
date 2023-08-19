using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public GameObject bulletPrefab;
    private Coroutine repeatFunction;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            repeatFunction = StartCoroutine(Repeating(collision.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (repeatFunction != null && collision.gameObject.name.Equals("Player"))
        {
            StopCoroutine(repeatFunction);
        }
    }

    private IEnumerator Repeating(GameObject go)
    {
        while (true)
        {
            Vector2 line = player.transform.position - this.transform.position;
            var bullet = Instantiate(bulletPrefab, this.transform.position, go.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = line;
            yield return new WaitForSeconds(1);
        }
    }
}
