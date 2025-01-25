using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject BallPrefab; // Top prefab
    public GameObject AcidBubblePrefab; // Acid Bubble prefab
    public Transform ThrowPoint; // F�rlatma noktas�
    public float ThrowForce = 10f; // F�rlatma g�c�

    private enum ThrowMode { Ball, AcidBubble } // F�rlatma modlar�
    private ThrowMode currentMode = ThrowMode.Ball; // Varsay�lan olarak top modu

    private void Update()
    {
        // Mod de�i�tirme
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1'e bas�ld���nda top moduna ge�
        {
            currentMode = ThrowMode.Ball;
            Debug.Log("Top moduna ge�ildi.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2'ye bas�ld���nda Acid Bubble moduna ge�
        {
            currentMode = ThrowMode.AcidBubble;
            Debug.Log("Acid Bubble moduna ge�ildi.");
        }

        // Sol t�kla f�rlatma
        if (Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    private void Throw()
    {
        // Mouse'un pozisyonunu d�nya koordinatlar�nda al
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini s�f�rla ��nk� 2D bir oyun yap�yoruz.

        // Mouse pozisyonuna do�ru y�n� hesapla
        Vector2 direction = (mousePosition - ThrowPoint.position).normalized;

        GameObject projectile = null;

        // Se�ilen moda g�re prefab olu�tur
        switch (currentMode)
        {
            case ThrowMode.Ball:
                projectile = Instantiate(BallPrefab, ThrowPoint.position, Quaternion.identity);
                break;
            case ThrowMode.AcidBubble:
                projectile = Instantiate(AcidBubblePrefab, ThrowPoint.position, Quaternion.identity);
                break;
        }

        // F�rlatma g�c� uygula
        if (projectile != null)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * ThrowForce, ForceMode2D.Impulse);
            }

            // Mermiyi mouse'a do�ru d�nd�r (iste�e ba�l�)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
