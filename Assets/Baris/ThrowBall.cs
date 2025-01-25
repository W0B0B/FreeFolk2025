using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballPrefab; // F�rlat�lacak top prefab
    public Transform throwPoint; // Topun f�rlat�laca�� nokta
    public float throwForce = 10f; // Topun f�rlatma g�c�

    void Update()
    {
        // Sol fare tu�una bas�ld���nda topu f�rlat
        if (Input.GetMouseButtonDown(0)) // 0: Sol t�k
        {
            Throw();
        }
    }

    void Throw()
    {
        // Fare pozisyonunu d�nya koordinatlar�na �evir
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini s�f�rla (2D oyun oldu�u i�in)

        // Topu olu�tur
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, Quaternion.identity);

        // Topun Rigidbody2D'sini al
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // F�rlatma y�n�n� hesapla
        Vector2 throwDirection = (mousePosition - throwPoint.position).normalized;

        // Topa kuvvet uygula
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            // Topu 5 saniye sonra yok et
            Destroy(ball, 5f);
    }
}
