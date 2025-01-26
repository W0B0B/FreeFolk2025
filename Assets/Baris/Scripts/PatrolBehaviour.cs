using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    public float speed;
    public float rayDist;
    private bool movingRight;
    public Transform groundDetect;

    void Update()
    {
        // Karakteri sa�a veya sola hareket ettir
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Yere temas kontrol�
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

        // E�er karakter zeminden d���yorsa
        if (groundCheck.collider == false)
        {
            // Karakterin y�n�n� de�i�tir
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}

