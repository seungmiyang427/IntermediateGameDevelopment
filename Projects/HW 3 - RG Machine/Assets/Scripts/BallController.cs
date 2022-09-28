using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Space();
    }

    public void Space()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.gravityScale = 1;
        }
    }

}
