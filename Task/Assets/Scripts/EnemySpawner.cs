using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private float randomX, randomY;
    private Vector3 newPos;
    private Quaternion newRot;
    public TextMeshProUGUI waveInt;

    private int wave;

    private int enemySpawnAmount;

    void Start()
    {
        enemySpawnAmount = 5;
        InvokeRepeating("SummonWave", 1, 1);
    }

    private void SummonWave()
    {

        if (StaticVars.EnemiesKilled >= enemySpawnAmount - StaticVars.WaveIncrement || enemySpawnAmount == StaticVars.FirstWaveAmount)
        {

            StaticVars.EnemiesKilled = 0;
            wave += 1;
            waveInt.text = wave.ToString();

            for (int i = 0; i < enemySpawnAmount; i++)
            {
                SummonEnemy();
            }

            enemySpawnAmount += StaticVars.WaveIncrement;

        } 
    }
    private void SummonEnemy()
    {

        RandomCoordGiver();

        transform.position = new Vector3(randomX, randomY, -1);
        newPos = transform.position;
        newRot = transform.rotation;
        Instantiate(enemy, newPos, newRot);
    }

    private void RandomCoordGiver()
    {
        randomX = Random.Range(-StaticVars.EastEdgeLimit + 2, StaticVars.EastEdgeLimit - 2);
        randomY = Random.Range(-StaticVars.NorthEdgeLimit + 2, StaticVars.NorthEdgeLimit - 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.collider.tag != "Player" || collision.collider.tag != "Missile")
            StopAllCoroutines();
    }
}
