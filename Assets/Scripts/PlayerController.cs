using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AudioManager audioManager; // Ссылка на AudioManager

    private void Start()
    {
        // Получаем ссылку на AudioManager
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddScore(2); // Увеличиваем счет
            }
            audioManager.PlayCoinSound(); // Воспроизводим звук при сборе монеты
            Destroy(collision.gameObject); // Удаляем монету
        }
        else if (collision.CompareTag("Fire"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver(); // Вызываем метод проигрыша
            }
            audioManager.PlayFireSound(); // Воспроизводим звук при попадании в огонь
        }
    }
}
