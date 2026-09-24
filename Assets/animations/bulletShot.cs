using UnityEngine;

public class bulletShot : MonoBehaviour
{
    float t = 0;
    float maxT = 0.3f;
    [SerializeField] private GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > maxT)
        {
            float a = Random.Range(1.25f * Mathf.PI, 1.75f * Mathf.PI);
            //float a = 1.5f * Mathf.PI;
            Vector2 vel = new Vector2(Mathf.Cos(a),Mathf.Sin(a));
            GameObject IBullet = Instantiate(bullet, new Vector3(-0.94f,5.18f,0),Quaternion.identity);
            IBullet.GetComponent<Rigidbody2D>().linearVelocity =5 * vel;
            IBullet.transform.eulerAngles = a*Mathf.Rad2Deg * Vector3.forward;
            maxT += 0.3f;
        }
        else if(t > 5)
        {
            Destroy(gameObject);
        }
    }
}
