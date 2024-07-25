using System;
using UnityEngine;


public class InsectController : MonoBehaviour
{

    public static Action onTouched;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            onTouched?.Invoke();
        }
    }


}
