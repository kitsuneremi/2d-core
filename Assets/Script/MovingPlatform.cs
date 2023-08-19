using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentIndex = 0;

    [SerializeField] private float speed = 2f;

    void Update()
    {
        if (Vector2.Distance(waypoints[currentIndex].transform.position, this.transform.position) < .1f)
        {
            currentIndex++;
            if(currentIndex >= waypoints.Length)
            {
                currentIndex = 0;
            }
        }

        this.transform.position = Vector2.MoveTowards(this.transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);

    }
}
