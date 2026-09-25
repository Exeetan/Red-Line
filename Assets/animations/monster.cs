using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

public class monster : MonoBehaviour
{
    float t = 0;
    int lives = 5;
    int abb = 0;
    SpriteRenderer sp;
    [SerializeField] private GameObject[] attacks;
    [SerializeField] private GameObject explosionAnim;
    [SerializeField] private GameObject explosionSound;

    [SerializeField] private GameObject dynamite;

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
        if(t > 2) 
        {
            t -= 2;
            GameObject a = attacks[Random.Range(0, attacks.Length)];
            Instantiate(attacks[1]);
            if(lives < 4 && lives > 1) Instantiate(attacks[1]);
            if (lives < 2) t++;
            abb++;
            if (abb > 5 && GameObject.FindGameObjectsWithTag("dynamite").Length == 0) Instantiate(dynamite, new Vector3(Random.Range(-6.8f, 5.7f), Random.Range(0.5f, -5.2f), 0), Quaternion.identity);
            if(!bulletShot.doing)
            {
                int p = Random.Range(0, 4);
                if (p == 1) Instantiate(attacks[3]);
            }
            if (!armHit.doing)
            {
                int p = Random.Range(0, 4);
                if (p == 1) Instantiate(attacks[0]);
            }
            //if (a.GetComponent<armAttack>() == null || a.GetComponent<armAttack>() != null && !armAttack.doing) Instantiate(attacks[Random.Range(0, attacks.Length)]);
        }
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
