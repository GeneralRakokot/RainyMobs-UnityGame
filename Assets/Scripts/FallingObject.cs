using UnityEngine;

public enum ObjectType
{
    Block, Enemy, Friend,
}

public class FallingObject : MonoBehaviour
{
    [Header("Тип")]
    public ObjectType type;

    [Header("Основные настройки")]
    public float resetYPosition = 10f;
    public float destroyYPosition = -10f;
    public float maxTiltAngle = 10f;

    private Rigidbody2D rb;
    private float minX, maxX;

    // Новое значение для замедленного падения:
    public float fallSpeed = -2.5f; // уменьшаем вдвое по сравнению с исходным (-5f)

    void Start()
    {
        // Настройка границ камеры
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 leftBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 rightTop = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            minX = leftBottom.x;
            maxX = rightTop.x;
        }

        // Установка случайного наклона
        float randomTiltZ = Random.Range(-maxTiltAngle, maxTiltAngle);
        transform.rotation = Quaternion.Euler(0, 0, randomTiltZ);

        rb = GetComponent<Rigidbody2D>();

        // Устанавливаем замедленную вертикальную скорость при телепортации
        rb.linearVelocity = new Vector3(0, fallSpeed, 0);
    }

    void Update()
    {
        if (transform.position.y <= destroyYPosition)
        {
            // Телепортируем в новую позицию
            float randomX = Random.Range(minX, maxX);
            transform.position = new Vector3(randomX, resetYPosition, transform.position.z);

            // Обновляем наклон
            float randomTiltZ = Random.Range(-maxTiltAngle, maxTiltAngle);
            transform.rotation = Quaternion.Euler(0, 0, randomTiltZ);

            // Возвращаем замедленную скорость падения
            rb.linearVelocity = new Vector3(0, fallSpeed, 0);
        }
        else
        {
            // Можно оставить без изменений - гравитация будет действовать как обычно
        }
    }

    public void OnClick()
    {
        string[] clicks = { "click0", "click1", "click2", "click3", "click4", "click5", "click6", "click7"};
        SFXCore.Play(clicks, 0.25f);
    }
}