using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject BallPrefab; // Top prefab
    public GameObject AcidBubblePrefab; // Acid Bubble prefab
    public Transform ThrowPoint; // Fýrlatma noktasý
    public float ThrowForce = 10f; // Fýrlatma gücü

    private enum ThrowMode { Ball, AcidBubble } // Fýrlatma modlarý
    private ThrowMode currentMode = ThrowMode.Ball; // Varsayýlan olarak top modu

    private void Update()
    {
        // Mod deðiþtirme
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1'e basýldýðýnda top moduna geç
        {
            currentMode = ThrowMode.Ball;
            Debug.Log("Top moduna geçildi.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2'ye basýldýðýnda Acid Bubble moduna geç
        {
            currentMode = ThrowMode.AcidBubble;
            Debug.Log("Acid Bubble moduna geçildi.");
        }

        // Sol týkla fýrlatma
        if (Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    private void Throw()
    {
        // Mouse'un pozisyonunu dünya koordinatlarýnda al
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini sýfýrla çünkü 2D bir oyun yapýyoruz.

        // Mouse pozisyonuna doðru yönü hesapla
        Vector2 direction = (mousePosition - ThrowPoint.position).normalized;

        GameObject projectile = null;

        // Seçilen moda göre prefab oluþtur
        switch (currentMode)
        {
            case ThrowMode.Ball:
                projectile = Instantiate(BallPrefab, ThrowPoint.position, Quaternion.identity);
                break;
            case ThrowMode.AcidBubble:
                projectile = Instantiate(AcidBubblePrefab, ThrowPoint.position, Quaternion.identity);
                break;
        }

        // Fýrlatma gücü uygula
        if (projectile != null)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * ThrowForce, ForceMode2D.Impulse);
            }

            // Mermiyi mouse'a doðru döndür (isteðe baðlý)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
