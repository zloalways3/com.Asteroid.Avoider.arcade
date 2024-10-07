using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject objectToSpawn; // Ссылка на префаб объекта, который нужно создать
    private GameObject spawnedObject; // Ссылка на созданный объект

    private void Start()
    {
        SpawnObject(); // Вызываем метод для создания объекта при старте
    }

    private void SpawnObject()
    {
        // Создаем объект и позиционируем его в центре сцены
        spawnedObject = Instantiate(objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity);
        
        // Устанавливаем порядок отображения объекта, чтобы он был сверху
        spawnedObject.transform.SetAsLastSibling();

        // Удаляем объект через 3 секунды
        Destroy(spawnedObject, 3f);
    }
}
