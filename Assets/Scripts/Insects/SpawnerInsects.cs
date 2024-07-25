using System.Collections;
using UnityEngine;

public class SpawnerInsects : MonoBehaviour
{
    public static SpawnerInsects Instance;

    [SerializeField] GameObject[] _spawnMasOne;
    [SerializeField] GameObject[] _spawnMasTwo;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject _insect;
    [SerializeField] private float _spawnIntervalSec = 5;
    [SerializeField] private float _scaleMin;
    [SerializeField] private float _scaleMax;

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
        yield return new WaitForSeconds(1);
        if (_spawnIntervalSec > 1)
            _spawnIntervalSec -= 0.8f;
    }

    private void ResetTimeCorutine()
    {
        _spawnIntervalSec = 5;        
        StopCoroutine(ChangeTimeToSpawn());
        StartCoroutine(ChangeTimeToSpawn());
        StartCoroutine(SpawnFishWithInterval());
    }

    //�������� ������� ������ ������� ������� ������� ���������
    private IEnumerator SpawnFishWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnIntervalSec); // �������� � ��������
            RandDirection();
            GameObject spawnedFish = Instantiate(_insect, GetRandomPointToMove().transform.position, Quaternion.identity);
            Vector3 spawnPoint = new Vector3(spawnedFish.transform.localScale.x, spawnedFish.transform.localScale.y, spawnedFish.transform.localScale.z);
            spawnedFish.transform.localScale = spawnPoint * Random.Range(_scaleMin, _scaleMax);
        }
    }

}
