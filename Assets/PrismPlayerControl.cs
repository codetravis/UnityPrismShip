using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismPlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int colorCode = 0;
    public float colorChangeRate = 3f;
    public float lastColorChange = 0f;

    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;

    private Sprite[] playerSprites;

    public Rigidbody2D body;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerSprites = new Sprite[] {redSprite, greenSprite, blueSprite};
        SpriteRenderer shipSprite = gameObject.GetComponent<SpriteRenderer>();
        shipSprite.sprite = playerSprites[colorCode];
        lastColorChange = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Time.time > lastColorChange + colorChangeRate)
        {
            ChangeColor();
        }
    }

    void FixedUpdate()
    {
        body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
        if(body.position.x > 9.0f)
        {
            body.MovePosition(new Vector2(9.0f, body.position.y));
        } 
        else if (body.position.x < -9.0f)
        {
            body.MovePosition(new Vector2(-9.0f, body.position.y));
        }
        
        if (body.position.y < -5.0f)
        {
            body.MovePosition(new Vector2(body.position.x, -5.0f));
        }
        else if (body.position.y > 5.0f)
        {
            body.MovePosition(new Vector2(body.position.x, 5.0f));
        }

    }

    void ChangeColor()
    {
        if(colorCode >= 2)
        {
            colorCode = 0;
        }
        else
        {
            colorCode += 1;
        }

        SpriteRenderer shipSprite = gameObject.GetComponent<SpriteRenderer>();
        shipSprite.sprite = playerSprites[colorCode];
        lastColorChange = Time.time;
    }
}
