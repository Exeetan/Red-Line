using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class bgMove : MonoBehaviour
{
    float t = 0;
    Vector3 initPos;
    float[] c = new float[6];
    private void Start()
    {
        initPos = transform.position;
        Restart();
    }
    private void Update()
    {
        t += Time.deltaTime;
        if (t > 2 * Mathf.PI * c[0] * c[2] * c[4]) Restart();

        transform.position = initPos + new Vector3(c[0] * Mathf.Cos(c[1] * t*0.2f), c[2] * Mathf.Sin(c[3] * t* 0.2f), initPos.z);
        transform.eulerAngles = c[4] * Mathf.Sin(c[5] * t* 0.2f) * Vector3.forward;
    }

    void Restart()
    {
        t -= 2 * Mathf.PI;
        c = c.Select(x => Random.Range(1f, 5f)).ToArray();
        Debug.Log(c[0]);

    }
}
