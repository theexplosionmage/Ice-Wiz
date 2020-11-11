using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{

    public Image healthBar;

    private float health;

    private void Start()
    {
        health = StaticVars.PlayerHealth;
    }
    void Update()
    {
        healthBar.fillAmount = 0.1f * health;

        if (health <= 0)
        {
            StaticVars.GameOver = true;

            if (StaticVars.Score > PlayerPrefs.GetInt("HighScore", 0))
                PlayerPrefs.SetInt("HighScore", StaticVars.Score);

            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyMissile")
            health -= 1;
    }
}
