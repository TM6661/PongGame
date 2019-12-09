using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    Rigidbody2D rigidBody2d;

    //Besar gaya awal untuk mendorong bola
    private static float xInitialForce = 70f;
    private static float yInitialForce = 15f;

    //Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;
    
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        RestartGame();
        trajectoryOrigin = transform.position;

    }

    void ResetBall()
    {
        //reset posisi
        transform.position = Vector2.zero;
        //reset kecepatan
        rigidBody2d.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //Tentukan nilai y dari gaya antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        //Tentukan nilai acak dari nilai 0 (inklusif) dan 2 (ekslusif)
        float randomDirection = Random.Range(0, 2);

        //Jika nilai dibawah 1, bola bergerak ke kiri
        //Jika tidak, maka sebaliknya
        if (randomDirection < 1.0f)
        {
            rigidBody2d.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else
        {
            rigidBody2d.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        //Kembalikan bola ke posisi semula
        ResetBall();

        //Setelah 2 detik, berikan gaya pada bola
        Invoke("PushBall", 2);
    }

    //Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    //Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get
        {
            return trajectoryOrigin;
        }
    }
}
