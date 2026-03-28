using UnityEngine;

[CreateAssetMenu(fileName = "BulletPatternConfig", menuName = "BulletHell/Pattern")]
public class BulletPatternConfig : ScriptableObject
{
    public GameObject projectile;
    public int bulletCount = 10;
    public float bulletSpeed = 5f;

    public float startAngle = 0f;
    public float endAngle = 360f;

    public float timeBetweenBursts = 1f;
    public int totalBursts = 5;
}
