using UnityEngine;
using UnityEngine.InputSystem; // подключение нового Input System


public class FallingObject : MonoBehaviour
{
    [Header("Тип")]
    public ObjectType type;

    [Header("Основные настройки")]
    public float resetYPosition = 10f;
    public float destroyYPosition = -10f;
    public float maxTiltAngle = 10f;

    private Rigidbody2D rb;
    private Entity entity;
    private Drops drops;

    private float minX, maxX;

    // Новое значение для замедленного падения:
    public float fallSpeed = -2.5f; // уменьшаем вдвое по сравнению с исходным (-5f)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
        drops = GetComponent<Drops>();

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

    private float maxForce = 15f; // максимальная сила
    private float maxDistance = 2f; // максимальное расстояние

    public void ThrowFromTouch(Vector2 touchPoint)
    {
        // Текущая позиция объекта
        Vector2 objectPosition = rb.position + new Vector2(0, 1);

        // Расстояние между точкой касания и объектом
        float distance = Vector2.Distance(objectPosition, touchPoint);
        // Расчет силы с учетом расстояния
        float forceScale = 0f;
        if (distance < maxDistance)
        {
            forceScale = (1 - Mathf.Clamp01(distance / maxDistance));
        }
        // Если distance >= maxDistance, сила = 0
        // Направление от точки касания к объекту
        Vector2 direction = objectPosition - touchPoint;
        
        // Нормализация для получения направления без учета длины
        direction.Normalize();

        // Итоговая сила
        float forceMagnitude = maxForce * forceScale;

        // Применение импульса в противоположную сторону
        rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
        // Пример добавления небольшого крутящего момента
        float torqueAmount = 1f; // значение силы вращения, регулируйте по необходимости
        if (direction.x > 0)
        {
            torqueAmount = torqueAmount * -1;
        }
        rb.AddTorque(torqueAmount, ForceMode2D.Impulse);
    }

    public void OnClick(Vector2 touchPoint)
    {
        string[] clicks = { "click0", "click1", "click2", "click3", "click4", "click5", "click6", "click7"};
        SFXCore.Play(clicks, 0.25f);
        ThrowFromTouch(touchPoint);
        drops.ExtraDrop(100);
        entity.DoDamage(1);
        if (!entity.IsAlive())
        {
            drops.MainDrop(100);
            Destroy(gameObject);
        }
    }
}