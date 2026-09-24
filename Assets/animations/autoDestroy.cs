using System.Collections;
using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    public float deathTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(deathTimer());
    }

    IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
