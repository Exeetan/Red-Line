using System.Collections;
using UnityEngine;

public class monster : MonoBehaviour
{
    float t = 0;
    int lives = 5;
    SpriteRenderer sp;
    [SerializeField] private GameObject[] attacks;
    [SerializeField] private GameObject explosionAnim;
    [SerializeField] private GameObject explosionSound;


    cameraStuff c;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        c = Camera.main.GetComponent<cameraStuff>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 2) { t -= 2; Instantiate(attacks[Random.Range(0,attacks.Length)]); Instantiate(attacks[Random.Range(0, attacks.Length)]); }
    }

    public void hit()
    {
        lives--;
        if (lives == 0) Destroy(gameObject);
        Instantiate(explosionAnim, transform.position + new Vector3(Random.Range(-2,2), Random.Range(- 1,2)), Quaternion.identity);
        Instantiate(explosionAnim, transform.position + new Vector3(Random.Range(-2,2), Random.Range(- 1,2)), Quaternion.identity);
        Instantiate(explosionSound);

        StartCoroutine(c.cameraShake());
        
        StartCoroutine(Immortality());
    }
    IEnumerator Immortality()
    {
        sp.color -= new Color(0, 0, 0, 0.5f);

        yield return new WaitForSeconds(1);

        sp.color += new Color(0, 0, 0, 0.5f);
    }
}
