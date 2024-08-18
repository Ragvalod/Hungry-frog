using Unity.Cinemachine;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition Instance;
    [SerializeField] private Camera _camera;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Camera GetCamera()
    {
        return _camera;
    }

    public Transform GetPosition()
    {
        Transform transform = GetComponent<Transform>();
        return transform;
    }
}
