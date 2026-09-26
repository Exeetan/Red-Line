using UnityEngine;

public class bulletShot : MonoBehaviour
{
    float t = 0;
    float maxT = 0.3f;
    public static bool doing = false;
    Transform m;
    [SerializeField] private GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doing = true;
        m = GameObject.FindGameObjectWithTag("monster").transform;
        transform.SetParent(m.GetChild(0));
        transform.localPosition = new Vector3(-0.03f,0.25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > maxT)
        {
            float ca = -Mathf.Atan2(m.GetChild(0).position.y - 1.5f, m.GetChild(0).position.x);
            float a = Random.Range(ca - Mathf.PI*0.25f, ca + Mathf.PI * 0.25f);
            //float a = 1.5f * Mathf.PI;
            Vector2 vel = new Vector2(Mathf.Cos(a),Mathf.Sin(a));
            GameObject IBullet = Instantiate(bullet, transform.position,Quaternion.identity);
            IBullet.GetComponent<Rigidbody2D>().linearVelocity =5 * vel;
            IBullet.transform.eulerAngles = a * Mathf.Rad2Deg * Vector3.forward;
            maxT += 0.3f;
        }
        else if(t > 5)
        {
            doing = false;
            Destroy(gameObject);
        }
    }
}
