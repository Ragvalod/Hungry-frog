using System.Collections;
using UnityEngine;

public class SpawnerInsects : MonoBehaviour
{
    public static SpawnerInsects Instance;

    [Header("������ ������ ����� ������"), SerializeField] GameObject[] _spawnMasOne;
    [Header("������ ������ ����� ������"), SerializeField] GameObject[] _spawnMasTwo;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject[] _insects;
    [Header("����� ����� ������� ���������"), SerializeField] private float _spawnIntervalSec;
    [Header("����� �� ������� ����������� ����� ������ ��������� �����"), Range(0, 0.5f), SerializeField] private float _spawnDeferencelSec;
    [Header("����� ����� ������� ����������� ����� ����� �������"), Range(5, 20), SerializeField] private float _spawnDecrementlSec;
    [Header(""), SerializeField] private float _scaleMin;
    [SerializeField] private float _scaleMax;

    private float _tempSpawnIntervalSec;
    private bool _direction;

    private void OnEnable()
    {
        PlayerController.onLevelChanged += ResetTimeCorutine;
    }

    private void OnDisable()
    {
        PlayerController.onLevelChanged -= ResetTimeCorutine;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _tempSpawnIntervalSec = _spawnIntervalSec;

        int rand = Random.Range(1, 3);

        if (rand == 1)
            _direction = true;
        else
            _direction = false;

        StartCoroutine(SpawnFishWithInterval());
        StartCoroutine(ChangeTimeToSpawn());
    }

    private void RandDirection()
    {
        int rand = Random.Range(1, 3);

        if (rand == 1)
            _direction = true;
        else
            _direction = false;
    }

    public GameObject GetRandomPointToMove()
    {
        GameObject randomGameObjectOne = _spawnMasOne[Random.Range(0, _spawnMasOne.Length)];
        GameObject randomGameObjectTwo = _spawnMasTwo[Random.Range(0, _spawnMasTwo.Length)];

        GameObject curent;

        if (_direction)
        {
            curent = randomGameObjectOne;
            _direction = !_direction;
        }
        else
        {
            curent = randomGameObjectTwo;
            _direction = !_direction;
        }

        return curent;
    }

    private IEnumerator ChangeTimeToSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDecrementlSec);
            if (_spawnIntervalSec > 0.2f)
            {
                _spawnIntervalSec -= 0.1f;

            }
        }
    }

    private void ResetTimeCorutine()
    {
        _spawnIntervalSec = _tempSpawnIntervalSec - _spawnDeferencelSec;
    }

    private float _scale;
    private Vector3 _localScaleInsect;
    private int _typeInsect;
    private bool _isEnamy;

    //����� ������ ���� ���������� � ����������� �� �������
    private void TypeInsectToSize()
    {
        _scaleMin = PlayerController.PlayerScale().x - 5;
        //if (_scaleMin < 0)
        //{
        //    _scaleMin = 0;
        //}
        _scaleMax = PlayerController.PlayerScale().x + 5;

        //�������� ������������ ������ ����������
        _scale = Random.Range(_scaleMin, _scaleMax);
        if (_scale < 0)
        {
            _scale = 0;
            _localScaleInsect = new Vector3(PlayerController.PlayerScale().x + _scale, PlayerController.PlayerScale().x + _scale, PlayerController.PlayerScale().x + _scale);
        }
        else
            _localScaleInsect = new Vector3(_scale, _scale, _scale);



        //� ����������� �� ���������� ����� ������� �������� ��� ����������
        if (_localScaleInsect.x > PlayerController.PlayerScale().x)
        {
            _isEnamy = true;
            _typeInsect = Random.Range(2, 4);
        }
        else
        {
            _isEnamy = false;
            _typeInsect = Random.Range(0, 2);
        }

    }

    //�������� ������� ������ ������� ������� ������� ���������
    private IEnumerator SpawnFishWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnIntervalSec); // �������� � ��������

            Debug.Log("Spawn for second: " + _spawnIntervalSec);
            TypeInsectToSize();
            RandDirection();
            GameObject spawnedInsect = Instantiate(_insects[_typeInsect], GetRandomPointToMove().transform.position, Quaternion.identity);
            spawnedInsect.GetComponent<EnemyPointer>().EnemyPointerColor(_isEnamy);

            Vector3 spawnPoint = new Vector3(spawnedInsect.transform.localScale.x, spawnedInsect.transform.localScale.y, spawnedInsect.transform.localScale.z);
            spawnedInsect.transform.localScale = _localScaleInsect;
        }
    }

}
