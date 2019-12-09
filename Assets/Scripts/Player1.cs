using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerControl
{
    [SerializeField] Player2 player2;
    public int ballContact;
    public int scaleStock;
    public override void AddContact()
    {
        ballContact++;
        if (ballContact % 5 == 0)
        {
            scaleStock++;
        }
    }

    public void DefaultScale()
    {
        // gameObject.transform.lossyScale.Scale(new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
        gameObject.transform.localScale = Vector2.Scale(new Vector2(1f,1f), new Vector2(1f,1f));
    }

    public override void ResetContact()
    {
        ballContact = 0;
        scaleStock = 0;
        if (player2.scaleStock > 0 || player2.ballContact > 0)
        {
            player2.scaleStock = 0;
            player2.ballContact = 0;
        }
        // Debug.Log("Player 1 Contact : " + scaleStock);
        // Debug.Log("Player 1 Contact : " + player2.scaleStock);
    }

    public override void ScaleSize()
    {
        if (scaleStock > 0)
        {
            gameObject.transform.localScale = Vector2.Scale(new Vector2(1f,2f), new Vector2(1f,3f));
            scaleStock--;
            StartCoroutine(TimeUp(10));
        }
    }

    private IEnumerator TimeUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DefaultScale();
    }

    public override void ResetScale()
    {
        DefaultScale();
        player2.DefaultScale();
    }
}
