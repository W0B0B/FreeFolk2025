using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject[] BubblePrefabs; // Tüm Bubble prefab'lerini tutacak bir dizi
    public Transform ThrowPoint; // Fýrlatma noktasý
    public float ThrowForce = 10f; // Fýrlatma gücü

    private int currentBubbleIndex = 0; // Þu anki seçili Bubble türünün indeksi

    private void Update()
    {
        // Mod deðiþtirme (Bubble türünü deðiþtirme)
        if (Input.GetKeyDown(KeyCode.E)) // E tuþuyla bir sonraki Bubble'a geç
        {
            currentBubbleIndex = (currentBubbleIndex + 1) % BubblePrefabs.Length;
            Debug.Log("Seçili Bubble: " + BubblePrefabs[currentBubbleIndex].name);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) // Q tuþuyla bir önceki Bubble'a geç
        {
            currentBubbleIndex = (currentBubbleIndex - 1 + BubblePrefabs.Length) % BubblePrefabs.Length;
            Debug.Log("Seçili Bubble: " + BubblePrefabs[currentBubbleIndex].name);
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

        // Seçili Bubble prefab'ini oluþtur
        GameObject projectile = Instantiate(BubblePrefabs[currentBubbleIndex], ThrowPoint.position, Quaternion.identity);

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
