using UnityEngine;

public class MainMenuCleaner : MonoBehaviour
{
    void Start()
    {
        var dontDestroyObjects =FindObjectsOfType<Transform>(true);
        foreach (var obj in dontDestroyObjects)
        {
            if (obj.name == "GameManager" || obj.name == "GameUI" || obj.name == "PauseManagerObj")
            {
                Destroy(obj.gameObject);
                Debug.Log($"🧹 {obj.name} silindi.");
            }
        }
    }
}
