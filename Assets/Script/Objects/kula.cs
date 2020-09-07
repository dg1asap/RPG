using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kula : MonoBehaviour
{
    public float time;
    private void Update()
    {
 Destroy(gameObject,time);
   
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
