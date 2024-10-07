using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1; // Текущий уровень
    public int maxLevels = 2; // Максимальное количество уровней
    public GameObject[] levelConfigurations; // Массив объектов, представляющих различные уровни
    public GameObject levelOneObject; // Ссылка на GameObject, который нужно активировать

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Загружаю текущий уровень из памяти
        LoadLevel(currentLevel); // Загружаю уровень при запуске игры
    }

    // Метод для загрузки уровня внутри одной сцены
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex > maxLevels) return; // Если уровень превышает максимальный, ничего не делаю
        
        // Активирую текущий уровень
        if (levelIndex - 1 < levelConfigurations.Length)
        {
            levelConfigurations[levelIndex - 1].SetActive(true);
        }

        currentLevel = levelIndex; // Устанавливаю текущий уровень
        PlayerPrefs.SetInt("CurrentLevel", currentLevel); // Сохраняю текущий уровень
        PlayerPrefs.Save(); // Сохраняю изменения
    }

    // Метод для активации GameObject при нажатии на уровень 1
    public void ActivateLevelOneObject()
    {
        if (currentLevel == 1)
        {
            levelOneObject.SetActive(true); // Активирую нужный GameObject
        }
    }
    
    // Метод, который вызывается при нажатии на кнопку уровня 1
    public void OnLevelOneButtonPressed()
    {
        ActivateLevelOneObject(); // Активирую GameObject при нажатии на кнопку уровня 1
    }

    // Метод для разблокировки следующего уровня
    public void UnlockNextLevel()
    {
        if (currentLevel < maxLevels) // Если текущий уровень меньше максимального
        {
            currentLevel++; // Увеличиваю текущий уровень
            PlayerPrefs.SetInt("CurrentLevel", currentLevel); // Сохраняю прогресс
            PlayerPrefs.Save(); // Сохраняю изменения
        }

        LoadLevel(currentLevel); // Загружаю следующий уровень
    }
}
