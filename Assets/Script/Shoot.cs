using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class Shoot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    public Camera mainCamera;
    private SpriteRenderer sprite;
    [SerializeField] private GameObject Gun;
    private float angleToRotate;

    [SerializeField] private int maxBullet;
    private int bulletRemaining;


    [SerializeField] private Transform middlePoint;

    private bool refreshable = true;

    [SerializeField] private TextMeshProUGUI text;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        bulletRemaining = maxBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletRemaining < maxBullet && refreshable)
        {
            bulletRemaining++;
            text.text = "bullet remaining: " + bulletRemaining;
        }
        if (Input.GetMouseButtonDown(0) && bulletRemaining > 0)
        {
            // vị trí con trỏ chuột trên màn hình
            Vector3 mousePositionScreen = Input.mousePosition;

            // Chuyển tọa độ chuột thành một điểm trong không gian trò chơi
            Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, mainCamera.nearClipPlane));

            // Tính vector hướng giữa player và con trỏ chuột
            Vector3 directionToMouse = mousePositionWorld - transform.position;
            // Tính vector hướng giữa gun và con trỏ chuột
            Vector3 gunToMouse = mousePositionWorld - Gun.transform.position;
            if (directionToMouse.x < 0)
            {
                sprite.flipX = true;
                Gun.transform.position = new Vector2(this.transform.position.x - .8f, Gun.transform.position.y);
                angleToRotate = Mathf.Atan2(gunToMouse.y, gunToMouse.x) * Mathf.Rad2Deg + 15;
                if (angleToRotate < 90 && angleToRotate > 0)
                {
                    angleToRotate = 90;
                }
                else if (angleToRotate > -120 && angleToRotate < 0)
                {
                    angleToRotate = -120;
                }
            }
            else
            {
                sprite.flipX = false;
                Gun.transform.position = new Vector2(this.transform.position.x + .8f, Gun.transform.position.y);
                angleToRotate = Vector3.SignedAngle(transform.up, gunToMouse, Vector3.forward);
                angleToRotate = Mathf.Atan2(gunToMouse.y, gunToMouse.x) * Mathf.Rad2Deg - 15;
                if(angleToRotate > 90)
                {
                    angleToRotate = 90;
                }else if(angleToRotate < -60)
                {
                    angleToRotate = -60;
                }
            }
            // Áp dụng góc xoay lên transform.rotation
            Quaternion newRotation = Quaternion.Euler(0f, 0f, angleToRotate);
            Gun.transform.rotation = newRotation;

            bulletRemaining -= 1;
            text.text = "bullet remaining: " + bulletRemaining;
            refreshable = false;
            if (bulletRemaining == 0) 
            {
                Invoke("RefreshBullet", 2);
            }
            
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(directionToMouse.x, directionToMouse.y) * bulletSpeed;

        }
    }

    public void RefreshBullet()
    {
        Debug.Log("Invoke");
        refreshable = true;
    }
}
