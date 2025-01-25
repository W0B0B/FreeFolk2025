using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject[] BubblePrefabs; // T�m Bubble prefab'lerini tutacak bir dizi
    public Transform ThrowPoint; // F�rlatma noktas�
    public float ThrowForce = 10f; // F�rlatma g�c�

    private int currentBubbleIndex = 0; // �u anki se�ili Bubble t�r�n�n indeksi

    private void Update()
    {
        // Mod de�i�tirme (Bubble t�r�n� de�i�tirme)
        if (Input.GetKeyDown(KeyCode.E)) // E tu�uyla bir sonraki Bubble'a ge�
        {
            currentBubbleIndex = (currentBubbleIndex + 1) % BubblePrefabs.Length;
            Debug.Log("Se�ili Bubble: " + BubblePrefabs[currentBubbleIndex].name);
        }
        else if (Input.GetKeyDown(KeyCode.Q)) // Q tu�uyla bir �nceki Bubble'a ge�
        {
            currentBubbleIndex = (currentBubbleIndex - 1 + BubblePrefabs.Length) % BubblePrefabs.Length;
            Debug.Log("Se�ili Bubble: " + BubblePrefabs[currentBubbleIndex].name);
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

        // Se�ili Bubble prefab'ini olu�tur
        GameObject projectile = Instantiate(BubblePrefabs[currentBubbleIndex], ThrowPoint.position, Quaternion.identity);

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
