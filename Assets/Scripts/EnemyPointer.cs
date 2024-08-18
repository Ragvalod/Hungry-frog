using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPointer : MonoBehaviour
{
  //  [SerializeField] private Transform _worldPointer;
    [SerializeField] private Transform _pointerIconTransform;
    [SerializeField] private GameObject _enemyPointer;


    private Color _colorRed = Color.red;
    private Color _colorGreen = Color.green;
    private Transform _playerTransform;
    private Vector3 fromPlayerToEnemy;
    Camera _camera;
    Ray ray;
    private void Update()
    {
        CalculateVectorToPlaer();

    }

    //Метод расчета вектора от врага до игрока
    private void CalculateVectorToPlaer()
    {

        _playerTransform = PlayerPosition.Instance.GetPosition();
        fromPlayerToEnemy = transform.position - _playerTransform.position;
        ray = new Ray(_playerTransform.position, fromPlayerToEnemy);
       // Debug.DrawRay(_playerTransform.position, fromPlayerToEnemy);
        CalculateCameraPlane();
    }

    //Метод расчета плоскостей камеры
    private void CalculateCameraPlane()
    {
        _camera = PlayerPosition.Instance.GetCamera();
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistans = Mathf.Infinity;

        for (int i = 0; i < 4; i++)
        {
            if(planes[i].Raycast(ray, out float distance))
            {
                if (distance < minDistans)
                {
                    minDistans = distance;
                }
            }
             
        }

        minDistans = Mathf.Clamp(minDistans, 0, fromPlayerToEnemy.magnitude);

        Vector3 markerPosition = ray.GetPoint(minDistans);
        //_worldPointer.position = markerPosition;

        _pointerIconTransform.position = _camera.WorldToScreenPoint(markerPosition);

        Vector3 toEnemy = transform.position - _playerTransform.position;

        if(toEnemy.magnitude > minDistans)
        {
            _enemyPointer.SetActive(true);
        }
        else
        {
            _enemyPointer.SetActive(false);
        }
    }

    //Метод которий красит указатель в зависимости размера насекомого
    public void EnemyPointerColor(bool isEnemy)
    {
        if (isEnemy)
        {
            _enemyPointer.GetComponent<Image>().color = _colorRed;
        }
        else
        {
            _enemyPointer.GetComponent<Image>().color = _colorGreen;
        }
    }
}
