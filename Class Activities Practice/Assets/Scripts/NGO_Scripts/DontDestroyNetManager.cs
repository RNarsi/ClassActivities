using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
