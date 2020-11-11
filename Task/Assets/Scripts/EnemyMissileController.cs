using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileController : MonoBehaviour
{
    void Start()
    {
        Invoke("Disappear", 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            Destroy(gameObject);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }


}
