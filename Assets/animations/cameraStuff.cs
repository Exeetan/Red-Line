using System.Collections;
using UnityEngine;

public class cameraStuff : MonoBehaviour
{
    Vector3 initPos;
    private void Start()
    {
        initPos = transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IEnumerator cameraShake()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;

            transform.position = initPos + Mathf.Pow((1-t),2)*new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f));

            yield return new WaitForEndOfFrame();
        }
    }
}
