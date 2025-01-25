using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Properties")]
    public float Health;
    public float CurrentHealth;
    public float Damage;
    public float MovementSpeed;
    public float RunSpeed;
    public float JumpForce;


    public Animator EntityAnimator;
    public Rigidbody2D EntityBody;
    public Collider2D EntityCollider;
    private void Start()
    {
        EntityBody = GetComponent<Rigidbody2D>();
        EntityCollider = GetComponent<Collider2D>();
    }



}
