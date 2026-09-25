using System.Collections;
using UnityEngine;

public class cameraStuff : MonoBehaviour
{
    Vector3 initPos;
    GameObject p;
    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("player");
        StartCoroutine(softRot());
        initPos = transform.localPosition;
    }
    private void Update()
    {
        float t = (p.transform.position.y+30) / 20;
        Camera.main.backgroundColor = new Color(Mathf.Lerp(1,0.31f,t), Mathf.Lerp(1,0.13f,t), Mathf.Lerp(1, 0.13f, t), 1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IEnumerator cameraShake()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;

            transform.localPosition = initPos + Mathf.Pow((1-t),2)*new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.2f, 0.2f));

            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator cameraSwing()
    {
        float t = 0;
        while (t< 1)
        {
            t += Time.deltaTime;

            transform.parent.position = -Mathf.Sin(t*Mathf.PI)* Vector3.up;

            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator softRot()
    {
        float t = 0;
        t += Time.deltaTime;
        while(true)
        {
            if (t > 2 * Mathf.PI) t -= 2 * Mathf.PI;
            transform.eulerAngles = 0.5f * Mathf.Sin(t) * Vector3.forward;
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator transition(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        dir *= Time.deltaTime;
        while((transform.position - pos).sqrMagnitude > 0.1f) 
        {
            transform.position += dir;
            yield return new WaitForEndOfFrame();
        }
    }
}
