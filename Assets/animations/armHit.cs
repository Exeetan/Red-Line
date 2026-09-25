using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;
using System.Collections;
public class armHit : MonoBehaviour
{
    public static bool doing = false;
    float t = 0;
    enum states
    {
        charging, hit, wait, retire, destroy
    }
    states s = states.charging;
    GameObject warning;
    SpriteRenderer wsr;
    [SerializeField] private GameObject Sound;
    public Transform m;
    CircleCollider2D explosion;
    SpriteRenderer exsr;
    cameraStuff c;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        doing = true;
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 7.5f, 0);
        m = GameObject.FindGameObjectWithTag("monster").transform;
        //m.position = transform.position + 4.8f * Vector3.right;
        c = Camera.main.GetComponent<cameraStuff>();
    }
    void Start()
    {
        m.GetChild(0).GetComponent<Animator>().speed = 1;
        m.GetChild(0).GetComponent<Animator>().Play("armHitAnim",0,0f);
        warning = transform.GetChild(0).gameObject;
        wsr = warning.GetComponent<SpriteRenderer>();
        explosion = GameObject.FindGameObjectWithTag("explosion").GetComponent<CircleCollider2D>();
        exsr = explosion.transform.GetChild(0).GetComponent<SpriteRenderer>();

        //wsr.color = new Color(1, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        switch (s)
        {
            case states.charging:
                if(wsr.color.a < 0.5f) wsr.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
                exsr.color -= new Color(0, 0, 0, Time.deltaTime);
                m.position = Vector3.Lerp(m.position, (transform.position.x+5f) * Vector3.right,t);
                if(t > 1) { t -= 1; s = states.hit; Destroy(warning); Instantiate(Sound); explosion.enabled = false; StartCoroutine(c.cameraSwing()); }
                break;
            case states.hit:
                if (transform.position.y > -2) transform.position -= 30 * Time.deltaTime * Vector3.up;
                else { s = states.wait; t = 0; }
                break;
            case states.wait:
                if(t > 1) { t -= 1; s = states.retire; }
                break;
            case states.retire:
                exsr.color += new Color(0, 0, 0, 255* Time.deltaTime);
                m.position = Vector3.Lerp(m.position, Vector3.zero, 2*t);
                m.GetChild(0).GetComponent<Animator>().StartPlayback();
                m.GetChild(0).GetComponent<Animator>().speed = -1;
                if (t > 1) transform.position += 20 * Time.deltaTime * Vector3.up;
                else s = states.destroy;
                break;
            case states.destroy:
                explosion.enabled = true;
                doing = false;
                Destroy(gameObject);
                break;
        }
        
    }
}
