using UnityEngine;
public enum BubbleType
{
    acid,
}
[CreateAssetMenu(menuName = "Bubble")]
public class Bubble : ScriptableObject
{
    [Header("Bubble Properties")]
    public BubbleType Type;
    public GameObject BubblePrefab;
    public LayerMask InteractionLayer;
    public float Cooldown;
    public float Damage;



}
