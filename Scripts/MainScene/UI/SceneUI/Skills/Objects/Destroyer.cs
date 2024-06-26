using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(nameof(Tags.Obstacle)) ||
           other.gameObject.CompareTag(nameof(Tags.Bullets)))
        {
            Destroy(other.gameObject);
            int rnd = Random.Range(0, 4);
            Managers.Sound.Play(rnd == 0? Sounds.DestroyOn1:rnd == 1?Sounds.DestroyOn2:rnd==2?Sounds.DestroyOn3:Sounds.DestroyOn4, true);
        }
    }
}