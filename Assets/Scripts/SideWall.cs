using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    //Menambahkan score pemain jika berbenturan dengan dinding ini
    public PlayerControl player;
    [SerializeField]
    private GameManager gameManager;


    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        //jika object bernama Ball
        if (anotherCollider.name == "Ball")
        {
            //tambah score
            player.IncrementScore();
            player.ResetContact();
            player.ResetScale();
            //belum mencapai score maksimal
            if (player.Score < gameManager.maxScore)
            {
                //restart jika bola menyentuh dinding
                anotherCollider.gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
            }
        }
    }

}
