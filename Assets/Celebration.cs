using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Celebration : MonoBehaviour
{
    public GameObject fireworkPrefab;
    [SerializeField]
    float timer;

    public void CreateFirework(Transform spawnLocation)
    {
        GameObject newFirework = Instantiate(fireworkPrefab);
        newFirework.transform.position = spawnLocation.position;
        if(newFirework.TryGetComponent(out VisualEffect visualEffect))
        {
            StartCoroutine(Coroutine(newFirework,visualEffect));
        }
      
    }

    IEnumerator Coroutine(GameObject toDestroy,VisualEffect effect)
    {
        yield return new WaitForSeconds(timer);
        Destroy(toDestroy);
    }
}
