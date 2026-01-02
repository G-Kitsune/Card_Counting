using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Vector3 deck = new Vector3(-7, 0, 0);
    public Vector3 center = new Vector3(0, 0, 0);
    public Vector3 grave = new Vector3(7, 0, 0);
    public Vector3 playerHand = new Vector3(0, -7, 0);
    public Vector3 aiHand = new Vector3(0, 7, 0);

    public Vector3 moveSpeed = new Vector3(0, 0, 0);

    public float speed = 0.3f;

    public bool patrolEnded = false;
    

    // Start is called before the first frame update

    public void Jump(Vector3 destination)
    {
        transform.position = destination;
    }

    public IEnumerator Move(Vector3 startPosition, Vector3 destination)
    {
        yield return new WaitForSeconds(0.6f);

        //Debug.Log("Moving To " + destination.x + " " + destination.y + " From " + transform.position.x + " " + transform.position.y);
        

        float moveSpeedX = destination.x - startPosition.x;
        float moveSpeedY = destination.y - startPosition.y;

        if (moveSpeedX > 0f)
        {
            moveSpeedX = speed;
        }
        else if (moveSpeedX < 0)
        {
            moveSpeedX = -speed;
        }

        if (moveSpeedY > 0f)
        {
            moveSpeedY = speed;
        }
        else if (moveSpeedY < 0)
        {
            moveSpeedY = -speed;
        }

        moveSpeed = new Vector3(moveSpeedX, moveSpeedY, 0);


        while ((moveSpeed.x > 0 && transform.position.x < destination.x)
            || (moveSpeed.x < 0 && transform.position.x > destination.x)
            || (moveSpeed.y < 0 && transform.position.y > destination.y)
            || (moveSpeed.y > 0 && transform.position.y < destination.y))
        {
            transform.Translate(moveSpeed);
            yield return new WaitForFixedUpdate();
        }

        Jump(destination);
        yield return null;
    }

    public IEnumerator Patrol(Vector3 startPosition, Vector3 destination)
    {
        patrolEnded = false;

        yield return new WaitForSeconds(0.6f);

        //Debug.Log("Patrol To " + destination.x + " " + destination.y + " From " + transform.position.x + " " + transform.position.y);


        float moveSpeedX = destination.x - startPosition.x;
        float moveSpeedY = destination.y - startPosition.y;

        if (moveSpeedX > 0f)
        {
            moveSpeedX = speed;
        }
        else if (moveSpeedX < 0)
        {
            moveSpeedX = -speed;
        }

        if (moveSpeedY > 0f)
        {
            moveSpeedY = speed;
        }
        else if (moveSpeedY < 0)
        {
            moveSpeedY = -speed;
        }

        moveSpeed = new Vector3(moveSpeedX, moveSpeedY, 0);


        while ((moveSpeed.x > 0 && transform.position.x < destination.x)
            || (moveSpeed.x < 0 && transform.position.x > destination.x)
            || (moveSpeed.y < 0 && transform.position.y > destination.y)
            || (moveSpeed.y > 0 && transform.position.y < destination.y))
        {
            transform.Translate(moveSpeed);
            yield return new WaitForFixedUpdate();
        }

        Jump(destination);
        yield return new WaitForSeconds(0.3f);

        GetComponent<CardImageChanger>().cardShow = false;

        while ((-moveSpeed.x > 0 && transform.position.x < startPosition.x)
            || (-moveSpeed.x < 0 && transform.position.x > startPosition.x)
            || (-moveSpeed.y < 0 && transform.position.y > startPosition.y)
            || (-moveSpeed.y > 0 && transform.position.y < startPosition.y))
        {
            transform.Translate(-moveSpeed);
            yield return new WaitForFixedUpdate();
        }
        patrolEnded = true;
        Jump(startPosition);
        yield return null;
    }
}
