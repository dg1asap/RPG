using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class shooting : PowerUp
{
    public Camera cam;

    public Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public Inventory playerInventory;

    public float bulletForce = 20f;
    public float moveSpeed = 5f;
    Vector2 movement;
    Vector2 mousePos;

    // Update is called once per frame
    private void Start()
    {
        powerupSignal.Raise();
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
   
        if (Input.GetButtonDown("Fire1")&&playerInventory.coins > 0 && !EventSystem.current.IsPointerOverGameObject())
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
