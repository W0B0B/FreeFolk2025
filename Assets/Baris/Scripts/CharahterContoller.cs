using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Karakterin normal hareket hızı
    [SerializeField] private float jumpForce = 10f; // Zıplama kuvveti
    [SerializeField] private float speedMultiplier = 2f; // Shift ile hızlanma çarpanı
    [SerializeField] private float groundCheckDistance = 0.1f; // Zemin kontrol mesafesi
    [SerializeField] private LayerMask groundLayer; // Sadece bu layer'da zıplama yapılabilir

    Rigidbody2D rb; // Rigidbody2D referansı
    Animator animator;
    private float horizontalInput; // Yatay hareket girdisi
    private float defaultSpeed; // Varsayılan hız
    private bool IsGrounded;
    private bool IsJumping;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = movementSpeed; // Varsayılan hareket hızını kaydet
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        // Yatay hareket girdisi
        horizontalInput = Input.GetAxisRaw("Horizontal");


        // Zıplama kontrolü
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && !IsJumping)
        {
            IsJumping = true;
            Jump();
            animator.SetBool("IsJumping", !IsGrounded);
        }

        // Shift ile hızlanma
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed *= speedMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = defaultSpeed;
        }

        // Karakterin yönünü güncelle
        UpdateCharacterDirection();
    }

    private void FixedUpdate()
    {
// Karakterin hareketini uygula
rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

// Animator parametrelerini güncelle
animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); // Mutlak değer alındı
animator.SetFloat("yVelocity", rb.velocity.y);


        // Eğer karakter yere temas ederse zıplama durumunu sıfırla
        if (IsGrounded)
        {
            IsJumping = false;
        }
    }

    private void Jump()
    {
        // Zıplama işlemi
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void UpdateCharacterDirection()
    {
        // Karakterin sağa veya sola dönmesini kontrol et
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Sağ tarafa bak
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Sol tarafa bak
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
        animator.SetBool("IsJumping", !IsGrounded);
    }
    private void OnDrawGizmos()
    {
        // Zemin kontrolü için çizgi gösterimi
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + groundCheckDistance * Vector2.down);
    }
}
