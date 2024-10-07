using UnityEngine;
using System.Collections.Generic; // Для использования списка

public class RandomSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Назначьте префаб монеты в Инспекторе
    public GameObject firePrefab;  // Назначьте префаб огня в Инспекторе
    public RectTransform canvasRect; // Укажите RectTransform вашего Canvas
    public float spawnInterval = 2f; // Интервал между спавном

    private bool isSpawning = true; // Флаг для контроля спавна
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Список для хранения спавненных объектов

    private void Start()
    {
        StartSpawning(); // Запуск спавна при старте
    }

    private void SpawnObjects()
    {
        // Проверяем, продолжается ли спавн объектов
        if (!isSpawning)
            return;

        // Случайным образом выбираем между спавном монеты или огня
        GameObject objectToSpawn = Random.Range(0, 2) == 0 ? coinPrefab : firePrefab;

        // Генерируем случайную позицию для спавна сверху, избегая середины
        float spawnX = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        float spawnY = canvasRect.rect.height / 2; // Спавн сверху

        // Вычисляем координаты, чтобы не появляться в центре
        if (Mathf.Abs(spawnX) < 50) // 50 - порог для центральной области
        {
            spawnX = spawnX < 0 ? -50 : 50; // Сдвигаем позицию влево или вправо
        }

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        
        // Создаем объект и устанавливаем его позицию
        GameObject spawnedObject = Instantiate(objectToSpawn, canvasRect);
        spawnedObject.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

        // Добавляем созданный объект в список
        spawnedObjects.Add(spawnedObject);
    }

    public void StopSpawning()
    {
        isSpawning = false; // Останавливаем спавн
        CancelInvoke(nameof(SpawnObjects)); // Останавливаем циклический вызов метода спавна
    }

    public void StartSpawning()
    {
        isSpawning = true; // Разрешаем спавн
        CancelInvoke(nameof(SpawnObjects)); // На всякий случай отменяем предыдущий вызов
        InvokeRepeating(nameof(SpawnObjects), 0f, spawnInterval); // Перезапускаем циклический вызов метода спавна
    }

    public void ResetSpawner()
    {
        StopSpawning(); // Остановить текущий спавн
        DestroySpawnedObjects(); // Уничтожить все спавненные объекты
        StartSpawning(); // Запустить спавн заново
    }

    // Метод для уничтожения всех спавненных объектов
    public void DestroySpawnedObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj); // Уничтожить объект
            }
        }
        spawnedObjects.Clear(); // Очистить список
    }
}
