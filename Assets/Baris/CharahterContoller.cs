using UnityEngine;

public class CharahterContoller : Entity
{
    float horizontalInput;
    float _jumpCount = 1;
    float trueSpeed;
    [Range(1, 3)]
    [SerializeField] float speedMulti;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _groundLayer;
    bool _isJumping = true;

    private void Awake()
    {
        trueSpeed = MovementSpeed;
    }

    private void Update()
    {
        // Zemin kontrol� (karakterin yerde olup olmad���n� belirler)
        _isJumping = Physics2D.Raycast(transform.position, Vector2.down, _distance, _groundLayer);

        // Yatay giri� (sa�a veya sola hareket)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Z�plama kontrol�
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == true)
        {
            _isJumping = false;
            EntityBody.linearVelocityY += JumpForce;
        }

        // Ko�ma (Shift tu�una bas�ld���nda h�z art�r�l�r)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MovementSpeed *= speedMulti;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MovementSpeed = trueSpeed;
        }

        // Karakterin y�n�n� g�ncelle (Ad�m 3)
        UpdateCharacterDirection();
    }

    private void FixedUpdate()
    {
        // Karakterin hareketi
        EntityBody.linearVelocityX = horizontalInput * MovementSpeed;
    }

    private void UpdateCharacterDirection()
    {
        // Karakterin sola veya sa�a d�nmesini kontrol et
        if (horizontalInput > 0) // Sa� tarafa hareket ediyorsa
        {
            transform.localScale = new Vector3(1, 1, 1); // Sa� tarafa bak
        }
        else if (horizontalInput < 0) // Sol tarafa hareket ediyorsa
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sol tarafa bak
        }
    }

    private void OnDrawGizmos()
    {
        // Zemin kontrol� i�in �izgi g�sterimi
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _distance * Vector2.down);
    }
}
