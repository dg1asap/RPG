using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kula : MonoBehaviour
{
    private void Update()
    {
 Destroy(gameObject,0.5f);
   
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
