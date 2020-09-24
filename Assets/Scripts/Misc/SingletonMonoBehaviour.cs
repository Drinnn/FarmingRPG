using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance {
        get {
            return _instance;
        }
    }

    private static T _instance;

    protected virtual void Awake() {
        if (_instance == null)
            _instance = this as T;
        else
            Destroy(gameObject);
    }

}
