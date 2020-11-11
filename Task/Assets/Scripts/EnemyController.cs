
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject missile;
    public Transform missileSpawnPos;
    public Image healthBar;

    private GameObject player;
    private GameObject currentMissile;

    private float distance;
    private float health;

    private bool isMovingRandomly;
    private bool isDetectionMoving;
    private bool isAttacking;


    void Start()
    {
        health = StaticVars.EnemyHealth;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        distance = DistanceCalc(gameObject, player);

        healthBar.fillAmount = (float)(0.2f * health);

        if (health <= 0)
        {
            StaticVars.EnemiesKilled += 1;
            StaticVars.Score += 10;
            Destroy(gameObject);
        }

        if (distance > StaticVars.EnemyDetectionRange)
            NeutralPosition();
        if (distance <= StaticVars.EnemyDetectionRange && distance > StaticVars.EnemyAttackRange)
            DetectedPosition();
        if (distance <= StaticVars.EnemyAttackRange)
            AttackPosition();

        
    }

    private void NeutralPosition()
    {
        if (isMovingRandomly)
            return;
        InvokeRepeating("RandomMove", 0f, 0.2f);
        isMovingRandomly = true;
        isDetectionMoving = false;
        isAttacking = false;
    }

    private void DetectedPosition()
    {
        if (isDetectionMoving)
            return;
        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(player.transform.position, StaticVars.EnemyDetectedSpeed));
        isDetectionMoving = true;
        isMovingRandomly = false;
        isAttacking = false;
        CancelInvoke();
    }

    private void AttackPosition()
    {
        if (isAttacking)
            return;
        StopAllCoroutines();
        CancelInvoke();
        InvokeRepeating("Attack", 0.1f, 0.5f);
        isAttacking = true;
        isDetectionMoving = false;
        isMovingRandomly = false;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Missile")
            health -= 1;
    }

    private float DistanceCalc(GameObject a, GameObject b)
    {
        float dist = Mathf.Sqrt(Mathf.Pow((b.transform.position.x - a.transform.position.x), 2) + Mathf.Pow((b.transform.position.y - a.transform.position.y), 2));
        return dist;
    }

    private void RandomMove()
    {
        var xPos = Random.Range(transform.position.x - 2, transform.position.x + 2);
        var yPos = Random.Range(transform.position.y - 2, transform.position.y + 2);

        if (OutOfBoundaries(xPos, yPos))
            return;

        StopAllCoroutines();
        var randomLocation = new Vector3(xPos, yPos, transform.position.z);
        StartCoroutine(MoveCoroutine(randomLocation, StaticVars.EnemyRandomSpeed));
    }

    private void Attack()
    {
        var nextShootPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector2 lookDir = nextShootPos - missileSpawnPos.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        missileSpawnPos.rotation = Quaternion.Euler(0, 0, angle);
        currentMissile = Instantiate(missile, missileSpawnPos.position, missileSpawnPos.rotation);
        currentMissile.GetComponent<Rigidbody2D>().AddForce(missileSpawnPos.up * 5f, ForceMode2D.Impulse);
    }

    IEnumerator MoveCoroutine(Vector3 destination, float speed)
    {

        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
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
