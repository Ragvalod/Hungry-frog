using UnityEngine;

public class FishMowe : MonoBehaviour
{

    [SerializeField] private GameObject _lineToPoint;
    [SerializeField] private float _moveSpeedMin = 1;
    [SerializeField] private float _moveSpeedMax = 100;
    [SerializeField] private float _fishScale;

    private Vector3 direction;
    private Vector3 moveTo;
    float speed;
    private void Start()
    {
        
        speed = Random.Range(5, 50);
        moveTo = Spawner.Instance.GetRandomPointToMove().transform.position;
        direction = moveTo - transform.position;
        direction.Normalize();

    }
    private void Update()
    {
        Move();
        Lock();
        DestroyThisObject();


    }

    private void Move()
    {
       transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * speed);
    }

    private void Lock()
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);

    }

    private void DestroyThisObject()
    {
        if (moveTo == transform.position)
        {
            Destroy(gameObject);
        }

    }

}
