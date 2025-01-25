using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Properties")]
    public float Health = 100f; // Maksimum can
    public float CurrentHealth; // �u anki can
    public float Damage = 10f; // Verdi�i hasar
    public float MovementSpeed = 5f; // Y�r�me h�z�
    public float RunSpeed = 8f; // Ko�ma h�z�
    public float JumpForce = 10f; // Z�plama g�c�

    [Header("Components")]
    public Animator EntityAnimator; // Animasyon kontrolc�s�
    public Rigidbody2D EntityBody; // Fizik bile�eni
    public Collider2D EntityCollider; // �arp��ma bile�eni

    private void Start()
    {
        // Rigidbody ve Collider bile�enlerini otomatik olarak al
        EntityBody = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();

        // Ba�lang�� can�n� maksimum cana e�itle
        CurrentHealth = Health;
    }

    /// <summary>
    /// Hasar alma metodu
    /// </summary>
    /// <param name="damageAmount">Al�nan hasar miktar�</param>
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount; // Can� azalt
        Debug.Log($"{gameObject.name} hasar ald�: {damageAmount}. Kalan can: {CurrentHealth}");

        // E�er can s�f�r veya alt�na d��erse �l
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// �l�m metodu
    /// </summary>
    private void Die()
    {
        Debug.Log($"{gameObject.name} �ld�!");

        // �l�m animasyonu varsa tetiklenebilir
        if (EntityAnimator != null)
        {
            EntityAnimator.SetTrigger("Die");
        }

        // �ste�e ba�l�: Nesneyi yok et
        Destroy(gameObject, 0.5f); // 0.5 saniye sonra nesneyi yok et
    }

    /// <summary>
    /// Can yenileme metodu
    /// </summary>
    /// <param name="healAmount">Yenilenecek can miktar�</param>
    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount; // Can� art�r
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health; // Maksimum can� a�ma
        }

        Debug.Log($"{gameObject.name} iyile�ti: {healAmount}. G�ncel can: {CurrentHealth}");
    }
}
