using UnityEngine;
using UnityEngine.UI; // Для работы с UI элементами

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Ссылка на AudioSource для фоновой музыки
    public AudioSource coinSound; // Ссылка на AudioSource для звука при сборе монет
    public AudioSource fireSound; // Ссылка на AudioSource для звука при попадании в огонь

    // Кнопки для управления музыкой и звуками
    public Button musicToggleButton; // Кнопка для включения/выключения музыки
    public Button soundToggleButton; // Кнопка для включения/выключения звуков

    // Изображения для кнопок
    public Sprite musicOnSprite; // Изображение для кнопки музыки "включено"
    public Sprite musicOffSprite; // Изображение для кнопки музыки "выключено"
    public Sprite soundOnSprite; // Изображение для кнопки звука "включено"
    public Sprite soundOffSprite; // Изображение для кнопки звука "выключено"

    private bool isMusicOn = true; // Статус фоновой музыки
    private bool isSoundOn = true; // Статус звуков

    private void Start()
    {
        // Привязываю методы к кнопкам
        musicToggleButton.onClick.AddListener(ToggleMusic);
        soundToggleButton.onClick.AddListener(ToggleSound);

        // Устанавливаю начальные состояния
        UpdateMusicState();
        UpdateSoundState();
    }

    public void PlayCoinSound()
    {
        if (isSoundOn)
        {
            coinSound.Play(); // Воспроизводим звук при касании монеты
        }
    }

    public void PlayFireSound()
    {
        if (isSoundOn)
        {
            fireSound.Play(); // Воспроизводим звук при касании огня
        }
    }

    private void ToggleMusic()
    {
        isMusicOn = !isMusicOn; // Переключаю состояние музыки
        UpdateMusicState(); // Обновляю состояние музыки
    }

    private void UpdateMusicState()
    {
        if (isMusicOn)
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play(); // Воспроизводим музыку, если она не играет
            }
            backgroundMusic.volume = 1f; // Устанавливаю громкость на максимум
            musicToggleButton.GetComponent<Image>().sprite = musicOnSprite; // Устанавливаю изображение "включено"
        }
        else
        {
            backgroundMusic.volume = 0f; // Устанавливаю громкость на 0, чтобы "выключить" музыку
            musicToggleButton.GetComponent<Image>().sprite = musicOffSprite; // Устанавливаю изображение "выключено"
        }
    }

    private void ToggleSound()
    {
        isSoundOn = !isSoundOn; // Переключаю состояние звуков
        UpdateSoundState(); // Обновляю состояние звуков
    }

    private void UpdateSoundState()
    {
        if (isSoundOn)
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOnSprite; // Устанавливаю изображение "включено"
        }
        else
        {
            soundToggleButton.GetComponent<Image>().sprite = soundOffSprite; // Устанавливаю изображение "выключено"
        }
    }
}
