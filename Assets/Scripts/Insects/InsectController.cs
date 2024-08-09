using System;
using TMPro;
using UnityEngine;


public class InsectController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI curentPointValue;
    
    private int _pointsInsects;


    private void Start()
    {
        curentPointValue.text = PoitCurentInsect().ToString();
    }

    public static Action onTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            onTouched?.Invoke();
        }
    }

    public int PoitCurentInsect()
    {
        int scale = transform.localScale.GetHashCode();
        _pointsInsects = scale;
        return scale;
    }

}
