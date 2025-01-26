using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject[] BubblePrefabs;
    public Transform ThrowPoint;
    public float ThrowForce = 15f;

    public RadialMenuManager radialMenuManager; // RadialMenuManager referansý

    private void Update()
    {
        // RadialMenuManager'dan seçilen bubble indeksini al
        int currentBubbleIndex = radialMenuManager.selectedBubbleIndex;

        // Eðer radial menü aktifse veya fýrlatma izni yoksa, fýrlatma iþlemini yapma
        if (radialMenuManager.isRadialMenuActive)
        {
            return;
        }

        // Sol týk ile fýrlatma
        if (Input.GetMouseButtonDown(0))
        {
            Throw(currentBubbleIndex);
        }
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
