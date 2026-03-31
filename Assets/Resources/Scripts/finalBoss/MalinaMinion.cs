using System.Collections;
using UnityEngine;

public class MalinaMinion : MonoBehaviour
{
    private HealthComponent minionHealth;
    private RandomMovement mover;
    private ShootAtPlayer shooter;

    void Start()
    {
        minionHealth = GetComponent<HealthComponent>();
        mover = GetComponent<RandomMovement>();
        shooter = GetComponent<ShootAtPlayer>();

        if (mover != null) mover.StartMoving();
        if (shooter != null) shooter.StartShooting();
    }
}
