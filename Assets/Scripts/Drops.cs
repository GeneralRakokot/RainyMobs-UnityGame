using UnityEngine;

public class Drops : MonoBehaviour
{
    [Header("Основной дроп")]
    [SerializeField]
    private DropPrefab[] dropPrefabsMain;   // Массив префабов с шансами

    [Header("Дополнительный дроп")]
    [SerializeField]
    private DropPrefab[] dropPrefabsExtra;   // Массив префабов с шансами

    public void MainDrop(int dropChance)
    {
        float randOverall = Random.Range(0f, 100f);
        if (randOverall > dropChance)
        {
            // Не выпадает ничего, если не прошли общий шанс
            return;
        }

        foreach (var drop in dropPrefabsMain)
        {
            float rand = Random.Range(0f, 100f);
            if (rand <= drop.chance)
            {
                GameObject droped = Instantiate(drop.prefab, transform.position, Quaternion.identity);
                Rigidbody2D rbd = droped.GetComponent<Rigidbody2D>();
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                rbd.linearVelocity = rb.linearVelocity * new Vector2(1, -1);
            }
        }
    }

    public void ExtraDrop(int dropChance)
    {
        float randOverall = Random.Range(0f, 100f);
        if (randOverall > dropChance)
        {
            // Не выпадает ничего, если не прошли общий шанс
            return;
        }

        foreach (var drop in dropPrefabsExtra)
        {
            float rand = Random.Range(0f, 100f);
            if (rand <= drop.chance)
            {
                GameObject droped = Instantiate(drop.prefab, transform.position, Quaternion.identity);
                Rigidbody2D rbd = droped.GetComponent<Rigidbody2D>();
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                rbd.linearVelocity = rb.linearVelocity * new Vector2(-1, 1) * Random.Range(0.85f, 1.9f);
            }
        }
    }
}