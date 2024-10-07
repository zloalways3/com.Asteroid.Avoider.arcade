using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения
    private Rigidbody2D rb; // Ссылка на Rigidbody2D
    public RectTransform canvasRect; // RectTransform канваса для определения границ

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
    }

    private void Update()
    {
        // Обработка кликов на экран
        if (Input.GetMouseButtonDown(0)) // Если нажата левая кнопка мыши
        {
            Vector2 mousePosition = Input.mousePosition; // Получаем позицию мыши на экране

            // Определяем ширину канваса
            float canvasWidth = canvasRect.rect.width;

            // Если нажатие было на левой стороне экрана
            if (mousePosition.x < canvasWidth / 2)
            {
                MoveLeft();
            }
            else // Если нажатие было на правой стороне экрана
            {
                MoveRight();
            }
        }

        // Ограничиваем позицию игрока в пределах канваса
        LimitMovement();
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Движение влево
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Движение вправо
    }

    private void LimitMovement()
    {
        // Получаем границы канваса
        Vector3[] corners = new Vector3[4];
        canvasRect.GetWorldCorners(corners);

        float leftBoundary = corners[0].x + (transform.localScale.x / 2); // Левый край канваса
        float rightBoundary = corners[2].x - (transform.localScale.x / 2); // Правый край канваса

        // Ограничиваем движение по оси X
        float clampedX = Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
