using UnityEngine;
using TMPro; // Для работы с TextMeshPro

public class GameManager : MonoBehaviour
{
    public GameObject winPanel; // Панель выигрыша
    public GameObject losePanel; // Панель проигрыша
    public TMP_Text scoreText; // Текст для отображения счета
    public TMP_Text winScoreText; // Текст для отображения счета на панели выигрыша
    public TMP_Text loseScoreText; // Текст для отображения счета на панели проигрыша
    public LevelManager levelManager; // Ссылка на LevelManager
    private int score = 0; // Переменная для хранения текущего счета

    private void Start()
    {
        // Инициализация состояния игры
        losePanel.SetActive(false); // Скрыть панель проигрыша в начале игры
        winPanel.SetActive(false); // Скрыть панель выигрыша в начале игры
        UpdateScore(0); // Установить начальный счет
    }

    public void AddScore(int amount)
    {
        score += amount; // Увеличить счет на указанную величину
        UpdateScore(score); // Обновить отображение счета

        // Проверка на выигрыш
        if (score >= 50) // Предполагается, что цель — набрать 50 очков
        {
            ShowWinPanel(); // Показать панель выигрыша
        }
    }

    private void UpdateScore(int newScore)
    {
        scoreText.text = "Score: " + newScore; // Обновить текст счета на главном экране
        winScoreText.text = "Score: " + newScore + "/50"; // Обновить текст счета на панели выигрыша
        loseScoreText.text = "Score: " + newScore + "/50"; // Обновить текст счета на панели проигрыша
    }

    public void GameOver()
    {
        losePanel.SetActive(true); // Показать панель проигрыша
        StopSpawning(); // Остановить спавн объектов
        Time.timeScale = 0; // Остановить игру
    }

    public void WinGame()
    {
        ShowWinPanel(); // Показать панель выигрыша

        // Остановить игру
        Time.timeScale = 0;

        // Разблокировать следующий уровень
        if (levelManager != null)
        {
            levelManager.UnlockNextLevel(); // Разблокировать следующий уровень
        }
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true); // Показать панель выигрыша
        StopSpawning(); // Остановить спавн объектов
        Time.timeScale = 0; // Остановить игру
    }

    private void StopSpawning()
    {
        RandomSpawner spawner = FindObjectOfType<RandomSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning(); // Остановить спавн объектов
        }
    }

    public void OnRetryButtonPressed()
    {
        ResetGame(); // Сбросить игру
    }

    private void ResetGame()
    {
        // Сбросить счет и обновить текст счета
        score = 0; // Сбросить счет
        UpdateScore(0); // Обновить текст счета

        // Скрыть панели выигрыша и проигрыша
        winPanel.SetActive(false); // Скрыть панель выигрыша
        losePanel.SetActive(false); // Скрыть панель проигрыша

        // Остановить текущий спавн, если он работает
        StopSpawning();

        // Запустить спавн объектов снова
        RandomSpawner spawner = FindObjectOfType<RandomSpawner>();
        if (spawner != null)
        {
            spawner.ResetSpawner(); // Сброс спавна
        }

        Time.timeScale = 1; // Восстановить временной поток
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            // Если вы находитесь в редакторе Unity, остановить воспроизведение
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Если игра запущена, выйти из приложения
            Application.Quit();
        #endif
    }
}
