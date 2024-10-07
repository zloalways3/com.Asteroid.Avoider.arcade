using UnityEngine;
using UnityEngine.UI;

public class LevelManagerV2 : MonoBehaviour
{
    public Button[] levelButtons;  // Массив кнопок для каждого уровня
    public GameObject levelOneObject; // Ссылка на GameObject, который нужно активировать
    private int currentLevel = 1;  // Текущий уровень

    void Start()
    {
        // Сбрасываю сохраненные данные для теста (можно удалить после проверки)
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.SetInt("UnlockedLevel", 1);  // Сброс до 1 уровня
        
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);  // Получаю последний разблокированный уровень (по умолчанию - 1)
        Debug.Log("Разблокированный уровень: " + unlockedLevel);

        // Делаю кликабельными только разблокированные уровни
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= unlockedLevel)  // Если уровень разблокирован
            {
                levelButtons[i].interactable = true;  // Делаю кнопку кликабельной
                Debug.Log("Уровень " + (i + 1) + " разблокирован");
            }
            else
            {
                levelButtons[i].interactable = false;  // Блокирую кнопку
                Debug.Log("Уровень " + (i + 1) + " заблокирован");
            }
        }
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
        ResetLevelOneObject();  // Сначала сбрасываю объект
        ActivateLevelOneObject();  // Затем активирую объект после сброса
    }

    // Метод для сброса объекта уровня 1
    public void ResetLevelOneObject()
    {
        levelOneObject.SetActive(false);  // Деактивирую объект
    
        // Если у объекта есть компоненты, которые нужно сбросить (например, позиция, счетчики и т.д.),
        // можно добавить код для сброса здесь:
        // Например:
        // levelOneObject.transform.position = Vector3.zero;  // Сбрасываю позицию объекта

        // Если есть специфические компоненты, например, таймер или анимации, сбрасываю их так:
        // var timer = levelOneObject.GetComponent<TimerComponent>();
        // if (timer != null) 
        // {
        //     timer.ResetTimer();  // Обнуляю таймер
        // }
    }

    // Вызов при нажатии на кнопку NEXT для разблокировки следующего уровня
    public void UnlockNextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (unlockedLevel < levelButtons.Length)  // Если не все уровни разблокированы
        {
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);  // Разблокирую следующий уровень
            Debug.Log("Разблокирован следующий уровень: " + (unlockedLevel + 1));
        }
    }

    void Update()
    {
        // Обновляю состояние кнопок для гарантии
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
