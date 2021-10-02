using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    public SpriteRenderer playerSprite;

    private int spriteFacing = -1;

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(xInput, yInput).normalized;
        body.velocity = inputDir * speed;

        if (body.velocity.x != 0f && Mathf.Sign(body.velocity.x) != Mathf.Sign(spriteFacing))
        {
            spriteFacing *= -1;
            playerSprite.flipX = !playerSprite.flipX;
        }
    }
}
