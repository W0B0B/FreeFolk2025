using UnityEngine;

public class CharahterContoller : Entity
{
    float horizontalInput;
    float _jumpCount = 1;
    [SerializeField] float _distance;

    [SerializeField] LayerMask _groundLayer;
    bool _isJumping = true;


    private void Update()
    {
        _isJumping = Physics2D.Raycast(transform.position, Vector2.down, _distance, _groundLayer);
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == true)
        {
            _isJumping = false;
            EntityBody.linearVelocityY += JumpForce;
        }
    }
    private void FixedUpdate()
    {
        EntityBody.linearVelocityX = horizontalInput * MovementSpeed;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _distance * Vector2.down);
    }
}
