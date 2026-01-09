using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health = 20; // Здоровье блока

    public int DoDamage(int damage)
    {
        health -= damage;
        return health;
    }

    public bool IsAlive()
    {
        if (health > 0) return true;
        else return false;
    }
}
