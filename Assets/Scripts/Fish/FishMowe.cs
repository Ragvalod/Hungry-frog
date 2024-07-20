using UnityEngine;

public class FishMowe : MonoBehaviour
{
    [SerializeField] private GameObject[] _moveToPoints;
    [SerializeField] private GameObject _lineToPoint;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _fishScale;

    private GameObject _targetPointToMove;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPointToMove = _moveToPoints[Random.Range(0, _moveToPoints.Length)];
        //_targetPosition = new Vector3(_targetPointToMove.transform.position.x, 0 , _targetPointToMove.transform.position.y);
    }
    private void Update()
    {
        transform.Translate(_lineToPoint.transform.position * Time.deltaTime * _moveSpeed);
    }


}
