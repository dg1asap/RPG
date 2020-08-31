﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : PowerUp
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    Vector2 movement;
    public Camera cam;
    public Inventory playerInventory;
    Vector2 mousePos;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    // Update is called once per frame
    private void Start()
    {
        powerupSignal.Raise();
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1")&&playerInventory.coins > 0)
        {
            Shoot();
          
        }
    }
    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
    void Shoot()
    {
       GameObject bullet= Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
      Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        playerInventory.coins -= 1;
        powerupSignal.Raise();
    }

 
}