using UnityEngine;

public class armHit : MonoBehaviour
{
    float t = 0;
    enum states
    {
        charging, hit, wait, retire, destroy
    }
    states s = states.charging;
    GameObject warning;
    SpriteRenderer wsr;
    [SerializeField] private GameObject Sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 7.5f, 0);
    }
    void Start()
    {
        warning = transform.GetChild(0).gameObject;
        wsr = warning.GetComponent<SpriteRenderer>();
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
                if(t > 1) { t -= 1; s = states.hit; Destroy(warning); Instantiate(Sound); }
                break;
            case states.hit:
                if (transform.position.y > -2) transform.position -= 30 * Time.deltaTime * Vector3.up;
                else { s = states.wait; t = 0; }
                break;
            case states.wait:
                if(t > 1) { t -= 1; s = states.retire; }
                break;
            case states.retire:
                if (transform.position.y < 8) transform.position += 20 * Time.deltaTime * Vector3.up;
                else s = states.destroy;
                break;
            case states.destroy:
                Destroy(gameObject);
                break;
        }
        
    }
}
