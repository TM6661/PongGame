using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode downButton = KeyCode.S;
    public KeyCode upButton = KeyCode.W;
    public KeyCode scaleSize = KeyCode.Space;
    public float speed = 10f;

    //Batas atas dan bawah game scene (Batas bawah menggunakan minus(-))
    public float yBoundary = 9f;
    
    private Rigidbody2D rigidBody2d;
    private int score;

    // Titik tumbukan terakhir bola, untuk menampilkan variable2x fisika terkait tumbuhan tersebut
    private ContactPoint2D lastContactPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mendapatkan kecepatan raket
        Vector2 velocity = rigidBody2d.velocity;
        

        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0f;
        }

        if (Input.GetKeyUp(scaleSize))
        {
            ScaleSize();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        rigidBody2d.velocity = velocity;

        Vector3 position = transform.position;

        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }
        transform.position = position;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    public ContactPoint2D LastContactPoint
    {
        get
        {
            return lastContactPoint;
        }
    }

    //Ketika terjadi tumbukan bola, merekam kontaknya
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
            AddContact();
        }
    }

    public virtual void AddContact()
    {

    }

    public virtual void ResetContact()
    {

    }

    public virtual void ScaleSize()
    {
        
    }

    public virtual void ResetScale()
    {
        
    }
}
