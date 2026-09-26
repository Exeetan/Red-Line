using System.Collections;
using System.Reflection.Metadata;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class cameraStuff : MonoBehaviour
{
    Vector3 initPos;
    GameObject p;
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject closeDoor;
    [SerializeField] private GameObject[] delete;
    monster m;
    private void Start()
    {
        m = GameObject.FindGameObjectWithTag("monster").transform.GetChild(0).GetComponent<monster>();
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
        transform.parent.SetParent(null);
        //Vector3 dir = pos - transform.parent.position;
        Vector3 StartPos = transform.parent.position;
        //dir.Normalize();
        //dir *= 5*Time.deltaTime;
        float t = 0;
        while(t < 1) 
        {
            t += Time.deltaTime;
            bg.GetComponent<SpriteRenderer>().color += new Color(0,0,0, t*0.2f);
            transform.parent.position = Vector3.Lerp(StartPos, pos, t);
            yield return new WaitForEndOfFrame();
        }
        transform.parent.position = Vector3.zero;


        foreach (GameObject g in delete) Destroy(g);
        m.enabled = true;
        music.SetActive(true);
        closeDoor.SetActive(true);
    }

    public IEnumerator followPlayer(UnityEngine.Transform p)
    {
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            transform.parent.position = Vector3.Lerp(transform.parent.position, p.position, t);
            yield return new WaitForEndOfFrame();
        }
        transform.parent.SetParent(p);
        transform.parent.localPosition = Vector3.zero;
    }
}
