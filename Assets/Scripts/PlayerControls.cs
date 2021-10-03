using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private static event System.Action<bool> MovementPermissionChanged;

    public float speed;
    public Rigidbody2D body;
    public SpriteRenderer playerSprite;

    private int spriteFacing = -1;
    private bool movementAllowed = true;

    public static void SetMovementAllowed(bool isAllowed)
    {
        MovementPermissionChanged?.Invoke(isAllowed);
    }

    private void Awake()
    {
        MovementPermissionChanged += OnMovementPermissionChanged;
    }

    private void OnDestroy()
    {
        MovementPermissionChanged -= OnMovementPermissionChanged;
    }

    private void Update()
    {
        if (movementAllowed)
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
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    private void OnMovementPermissionChanged(bool isAllowed)
    {
        movementAllowed = isAllowed;
    }
}
