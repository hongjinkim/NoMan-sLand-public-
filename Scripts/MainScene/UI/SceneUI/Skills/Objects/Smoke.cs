using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(nameof(Tags.Bullets)))
            Destroy(other.gameObject);
    }
}