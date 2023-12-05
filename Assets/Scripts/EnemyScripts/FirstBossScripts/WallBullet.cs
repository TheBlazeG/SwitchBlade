using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBullet : MonoBehaviour
{
    [SerializeField] float wallBulletSpeed;
    private Rigidbody2D wallBulletRigidbody2d;
    private float timer = 15;
    void Start()
    {
        wallBulletRigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        wallBulletRigidbody2d.velocity = Vector2.right * wallBulletSpeed * Time.deltaTime;
    }
}
