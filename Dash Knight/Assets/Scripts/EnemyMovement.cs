using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int frames;
    public float speed = 0.003f;
    public float position = 1;
    public bool flip = true;

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

            if (frames == 60)
            {
                frames = 0;
                flip = !flip;
            }

            frames++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
