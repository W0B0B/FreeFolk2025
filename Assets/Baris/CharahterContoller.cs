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
        // Zemin kontrolü (karakterin yerde olup olmadýðýný belirler)
        _isJumping = Physics2D.Raycast(transform.position, Vector2.down, _distance, _groundLayer);

        // Yatay giriþ (saða veya sola hareket)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Zýplama kontrolü
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == true)
        {
            _isJumping = false;
            EntityBody.linearVelocityY += JumpForce;
        }

        // Koþma (Shift tuþuna basýldýðýnda hýz artýrýlýr)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            MovementSpeed *= speedMulti;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MovementSpeed = trueSpeed;
        }

        // Karakterin yönünü güncelle (Adým 3)
        UpdateCharacterDirection();
    }

    private void FixedUpdate()
    {
        // Karakterin hareketi
        EntityBody.linearVelocityX = horizontalInput * MovementSpeed;
    }

    private void UpdateCharacterDirection()
    {
        // Karakterin sola veya saða dönmesini kontrol et
        if (horizontalInput > 0) // Sað tarafa hareket ediyorsa
        {
            transform.localScale = new Vector3(1, 1, 1); // Sað tarafa bak
        }
        else if (horizontalInput < 0) // Sol tarafa hareket ediyorsa
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sol tarafa bak
        }
    }

    private void OnDrawGizmos()
    {
        // Zemin kontrolü için çizgi gösterimi
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _distance * Vector2.down);
    }
}
