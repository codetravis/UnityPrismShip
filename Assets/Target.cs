using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public int colorCode = 0;
    public int health = 0;
    static Text scoreCard;

    // Start is called before the first frame update
    void Start()
    {
        if(scoreCard == null)
        {
            scoreCard = GameObject.Find( "ScoreCard" ).GetComponentInChildren<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < -7.0f)
        {
            Reset();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "BULLET")
        {
            Bullet collidedBullet = collision.gameObject.GetComponent<Bullet>();
            if(collidedBullet.colorCode == colorCode)
                {
                // increase score here
                GameStatus.score += 10;
                scoreCard.text = "Score: " + GameStatus.score.ToString();
                Reset();
                }
        }
        else if (collision.gameObject.tag == "ENEMY")
        {
            
        }
        else
        {
            Reset();
        }
    }

    void Reset()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(-20, -20, 0);
        Rigidbody2D targetBody = gameObject.GetComponent<Rigidbody2D>();
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = 0f;
    }
}
