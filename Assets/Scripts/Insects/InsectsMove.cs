using UnityEngine;

public class InsectsMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeedMin = 1;
    [SerializeField] private float _moveSpeedMax = 100;
    [SerializeField] private float _insectScale;

    private Vector3 direction;
    private Vector3 moveTo;
    float speed;
    private void Start()
    {

        speed = Random.Range(10, 120);
        moveTo = SpawnerInsects.Instance.GetRandomPointToMove().transform.position;
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
