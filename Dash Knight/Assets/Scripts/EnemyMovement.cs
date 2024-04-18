using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int frames;
    public float speed = 0.003f;
    public float position = 1;
    public bool flip = true;
    private bool isFacingRight = true;
    private Rigidbody2D m_rigidBody;

    void FixedUpdate()
    {
        //Moves the enemy back and forth
        if (frames <= 60)
        {
            if (flip)
            {
                transform.Translate(position * speed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-position * speed * Time.deltaTime, 0, 0);
            }

            //Flip();

            if (frames == 60)
            {
                frames = 0;
                flip = !flip;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            frames++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Flips the players direction based on the direction of movement
    private void Flip()
    {
        if (isFacingRight && m_rigidBody.velocity.x < 0f || !isFacingRight && m_rigidBody.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
