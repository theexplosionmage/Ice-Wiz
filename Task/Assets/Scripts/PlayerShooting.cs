using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
    {
    private GameObject currentMissile;
    private Vector3 nextShootPos;


    public GameObject missile;
    public Transform missileSpawnPos;
    public Transform healthBarCanvas;

    private bool canShoot;

    [SerializeField] private float missileSpeed;
    private void Start()
    {
        canShoot = true;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(1) && canShoot)
        {
            nextShootPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            SpriteFlipper();
            Vector2 lookDir = nextShootPos - missileSpawnPos.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            missileSpawnPos.rotation = Quaternion.Euler(0, 0, angle);
            currentMissile = Instantiate(missile, missileSpawnPos.position, missileSpawnPos.rotation);
            currentMissile.GetComponent<Rigidbody2D>().AddForce(missileSpawnPos.up * missileSpeed, ForceMode2D.Impulse);
            canShoot = false;
            Invoke("Cooldown", StaticVars.Cooldown);
        }

        

    }



    private void SpriteFlipper()
    {
        if (nextShootPos.x < transform.position.x)
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

}
