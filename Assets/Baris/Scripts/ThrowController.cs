using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject[] BubblePrefabs;
    public Transform ThrowPoint;
    public float ThrowForce = 15f;
    private float throwCooldown = 1f; // 1 saniyelik bekleme s�resi
    private float lastSelectionTime = -0.5f; // Son se�im zaman�

    public RadialMenuManager radialMenuManager; // RadialMenuManager referans�

    private void Update()
    {
        // RadialMenuManager'dan se�ilen bubble indeksini al
        int currentBubbleIndex = radialMenuManager.selectedBubbleIndex;

        // Sol t�k ile f�rlatma, ancak 1 saniyelik bekleme s�resi dolmu�sa
        if (Input.GetMouseButtonDown(0) && !radialMenuManager.isRadialMenuActive && Time.time >= lastSelectionTime + throwCooldown)
        {
            Throw(currentBubbleIndex);
        }
    }

    public void NotifyBubbleSelected()
    {
        lastSelectionTime = Time.time; // Son se�im zaman�n� g�ncelle
    }

    private void Throw(int bubbleIndex)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - ThrowPoint.position).normalized;

        GameObject projectile = Instantiate(BubblePrefabs[bubbleIndex], ThrowPoint.position, Quaternion.identity);

        if (projectile != null)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * ThrowForce, ForceMode2D.Impulse);
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
