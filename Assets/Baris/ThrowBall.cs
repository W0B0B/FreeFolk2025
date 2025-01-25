using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballPrefab; // Fýrlatýlacak top prefab
    public Transform throwPoint; // Topun fýrlatýlacaðý nokta
    public float throwForce = 10f; // Topun fýrlatma gücü

    void Update()
    {
        // Sol fare tuþuna basýldýðýnda topu fýrlat
        if (Input.GetMouseButtonDown(0)) // 0: Sol týk
        {
            Throw();
        }
    }

    void Throw()
    {
        // Fare pozisyonunu dünya koordinatlarýna çevir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini sýfýrla (2D oyun olduðu için)

        // Topu oluþtur
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, Quaternion.identity);

        // Topun Rigidbody2D'sini al
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // Fýrlatma yönünü hesapla
        Vector2 throwDirection = (mousePosition - throwPoint.position).normalized;

        // Topa kuvvet uygula
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            // Topu 5 saniye sonra yok et
            Destroy(ball, 5f);
    }
}
