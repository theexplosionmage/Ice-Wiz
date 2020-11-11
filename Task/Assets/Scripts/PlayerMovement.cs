using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
    {
    private Vector3 nextMovePos;

    public Transform healthBarCanvas;

    [SerializeField] private float moveSpeed;

    void Update()
    {
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

    }

    IEnumerator MovePlayer(Vector3 destination, float speed) //Coroutine for moving the player
    {
        
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
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
