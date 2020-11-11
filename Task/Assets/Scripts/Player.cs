using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
    {
    private GameObject currentMissile;
    private Vector3 nextMovePos;
    //private Vector3 mouseClickCoords;

    public Transform healthBarCanvas;
    public Image healthBar;
    public GameObject missile;
    public Transform missileSpawnPos;

    private float health;
    private bool canShoot;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float missileSpeed;
    private void Start()
    {
        canShoot = true;
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
            //endgame
        }

        if (Input.GetMouseButtonDown(0))
        {
            var xMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            var yMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            if (!OutOfBoundaries(xMousePos, yMousePos))
            {
                nextMovePos = new Vector2(xMousePos, yMousePos);
                SpriteFlipper();
                var newCor = MovePlayer(nextMovePos, moveSpeed);
                StopAllCoroutines();
                StartCoroutine(newCor);
            }
            
        }

        if (Input.GetMouseButtonDown(1) && canShoot)
        {
            nextMovePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            SpriteFlipper();
            Vector2 lookDir = nextMovePos - missileSpawnPos.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            missileSpawnPos.rotation = Quaternion.Euler(0, 0, angle);
            currentMissile = Instantiate(missile, missileSpawnPos.position, missileSpawnPos.rotation);
            currentMissile.GetComponent<Rigidbody2D>().AddForce(missileSpawnPos.up * 10f, ForceMode2D.Impulse);
            canShoot = false;
            Invoke("Cooldown", StaticVars.Cooldown);
        }

        

    }

    IEnumerator MovePlayer(Vector3 destination, float speed) //Coroutine for moving the player
    {
        
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyMissile")
            health -= 1;
    }

    private void SpriteFlipper()
    {
        if (nextMovePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        healthBarCanvas.localScale = transform.localScale;
    }

    private void Cooldown()
    {
        canShoot = true;
    }

    private bool OutOfBoundaries(float x, float y)
    {
        if (x > StaticVars.EastEdgeLimit ||
            x < -StaticVars.EastEdgeLimit ||
            y > StaticVars.NorthEdgeLimit ||
            y < -StaticVars.NorthEdgeLimit)
            return true;

        return false;

    }
}
