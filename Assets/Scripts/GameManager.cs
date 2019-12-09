using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player 1
    public PlayerControl player1;
    private Rigidbody2D player1RigidBody;

    //Player 2
    public PlayerControl player2;
    private Rigidbody2D player2RigidBody;

    //Bola 
    public BallControl ball;
    private Rigidbody2D ballRigidBody;
    private CircleCollider2D ballCollider;
    
    //Skor Max
    public int maxScore;

    //Apakah debug window ditampilkan 
    private bool isDebugWindowShown = false;

    //Object menggambarkan prediksi lintasan bola
    public Trajectory trajectory;

    [SerializeField] 
    private Player1 player1_ref;
    [SerializeField] 
    public Player2 player2_ref;

    private void Start()
    {
        player1RigidBody = player1.GetComponent<Rigidbody2D>();
        player2RigidBody = player2.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
    }

    void OnGUI()
    {
        //Tampilkan skor pemain 1 di kiri atas dan pemain 2 di kanan atas
        GUI.Label(new Rect(Screen.width / 2 - 470 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 - 500 - 30, 20, 100, 100), "Score");
        GUI.Label(new Rect(Screen.width / 2 + 470 + 12, 20, 100, 100), "" + player2.Score);
        GUI.Label(new Rect(Screen.width / 2 + 500 + 30, 20, 100, 100), "Score");

        //Tampilkan stock power up scale ke layar 
        GUI.Label(new Rect(Screen.width / 2 - 470 - 12, 650, 100, 100),"" + player1_ref.scaleStock);
        GUI.Label(new Rect(Screen.width / 2 - 500 - 30, 650, 100, 100), "Scale");
        GUI.Label(new Rect(Screen.width / 2 + 470 + 12, 650, 100, 100),"" + player2_ref.scaleStock);
        GUI.Label(new Rect(Screen.width / 2 + 500 + 30, 650, 100, 100), "Scale");

        //Tombol restart untuk memulai game dari awal
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 30), "RESTART"))
        {
            //Ketika tombol restart ditekan, reset kedua pemain
            player1.ResetScore();
            player2.ResetScore();

            //Restart game
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        //Jika pemain 1 menang (mencapai skor maksimal)
        if (player1.Score == maxScore)
        {
            //Menampilkan text pemain 1 menang
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "Player 1 Win");

            //Tampilkan bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        //Jika pemain 2 menang (mencapai skor maksimal)
        else if (player2.Score == maxScore)
        {
            //Menampilkan text pemain 2 menang
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "Player 2 Win");

            //Tampilkan bola ke tengah
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            float ballMass = ballRigidBody.mass;
            Vector2 ballVelocity = ballRigidBody.velocity;
            float ballSpeed = ballRigidBody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction" + ballFriction + "\n" +
                "Last impulse from Player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")" + "\n" +
                "Last impulse from Player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")" + "\n";

            //Tampilan debug GUI
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110),debugText,guiStyle);

            //Kembalikan warna lama pada GUI
            GUI.backgroundColor = oldColor;
        }

        if(GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53),"TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
            trajectory.ballAtCollision.SetActive(false);
            
        }
    }
}
