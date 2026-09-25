using System.Linq;
using UnityEngine;

public class debrisPunch : MonoBehaviour
{
    float t = 0;
    SpriteRenderer sr;
    Rigidbody2D[] debris;
    [SerializeField] private GameObject Sound;

    enum states
    {
        charging, wait, destroy
    }

    cameraStuff c;

    states s = states.charging;
    private void Awake()
    {
        //Vector3 pos = new Vector3(Random.Range(-6.8f, 5.7f), Random.Range(0.5f, -5.2f), 0);
        //while (Physics2D.OverlapCircle(pos, 2))
        //{
        //    pos = new Vector3(Random.Range(-6.8f, 5.7f), Random.Range(0.5f, -5.2f), 0);
        //}
        //transform.position = pos;
        transform.position = new Vector3(Random.Range(-6.8f, 5.7f), Random.Range(0.5f, -5.2f), 0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        debris = GetComponentsInChildren<Rigidbody2D>(true);
        c = Camera.main.GetComponent<cameraStuff>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        switch (s)
        {
            case states.charging:
                if (sr.color.a < 0.5f) sr.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
                if (t > 1) 
                {
                    t -= 1;

                    GetComponent<CircleCollider2D>().enabled = true;
                    float a = Random.Range(0, Mathf.PI / 3);
                    foreach(Rigidbody2D d in debris)
                    {
                        d.gameObject.SetActive(true);
                        d.linearVelocity = 8 * new Vector2(Mathf.Cos(a),Mathf.Sin(a));
                        a += Mathf.PI / 3;
                        d.angularVelocity = Random.Range(0f, 360f);
                    }
                    Instantiate(Sound);
                    StartCoroutine(c.cameraShake());
                    s = states.wait;
                }
                break;
            case states.wait:
                if (sr.color.a > 0) sr.color -= new Color(0, 0, 0, Time.deltaTime);
                if(t > 0.75f && GetComponent<CircleCollider2D>().enabled) GetComponent<CircleCollider2D>().enabled = false;
                if (t > 2) s = states.destroy;
                break;
            case states.destroy:
                Destroy(gameObject);
                break;
        }
    }
}
