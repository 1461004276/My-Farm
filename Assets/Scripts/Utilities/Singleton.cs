using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = (T)this;
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}