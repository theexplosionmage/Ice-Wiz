using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    void Start()
    {
        Invoke("Disappear", 0.3f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
            Destroy(gameObject);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
   

}
