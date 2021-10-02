using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(xInput, yInput).normalized;
        body.velocity = inputDir * speed;
    }
}
