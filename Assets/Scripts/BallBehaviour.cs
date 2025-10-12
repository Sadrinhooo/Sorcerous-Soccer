using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private GameObject _currentBallHolder;

    public GameObject CurrentBallHolder
    {
        get
        {
            return _currentBallHolder;
        }

        set
        {
            if (_currentBallHolder != null)
            {
                _currentBallHolder = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ResetBall"))
        {
            gameObject.transform.position = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
