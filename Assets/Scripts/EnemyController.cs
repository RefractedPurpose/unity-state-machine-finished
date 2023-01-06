using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum EnemyState {
        Idle,
        Moving
    }
    private EnemyState currentState = EnemyState.Idle;
    private SpriteRenderer sr;
    //value found in debuglog
    private const float Sight_Distance = 7.0f;
    //the 2 points enemy moves between while moving
    private const float RIGHT_MAX = 27.5f;
    private const float LEFT_MAX = 22.5f;

    private int direction = -1;
    private float xSpeed = 0.02f;
    public GameObject player;

    void IdleState(float distance) {
        sr.color = Color.white;
        //switch to moving in sight range
        if (distance <= Sight_Distance) {
            currentState = EnemyState.Moving;
        }
    }

    void MovingState(float distance) {
        sr.color = Color.yellow;

        //move back and forth
        if (transform.position.x >= RIGHT_MAX) {
            direction = -1;
        } else if (transform.position.x <= LEFT_MAX) {
            direction = 1;
        }
        transform.position = new Vector3(transform.position.x + direction * xSpeed, transform.position.y, transform.position.z);
        //switch back to idle when out of sight range
        if (distance > Sight_Distance) {
            currentState = EnemyState.Idle;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance  = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (currentState == EnemyState.Idle) {
            IdleState(distance);
        } else if (currentState == EnemyState.Moving) {
            MovingState(distance);
        }
        //Log distance for ideal sight range
        Debug.Log(distance);
    }
}
