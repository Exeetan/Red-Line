using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;

public class monster : MonoBehaviour
{
    float t = 0;
    public float reposT = 0;
    public int lives = 8;
    int abb = 0;
    bool dead = false;
    SpriteRenderer[] sp;
    [SerializeField] private GameObject[] attacks;
    [SerializeField] private GameObject explosionAnim;
    [SerializeField] private GameObject explosionSound;
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject theend;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject dynamite;

    cameraStuff c;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponentsInChildren(typeof(SpriteRenderer),true).Select(x => (SpriteRenderer)x).ToArray();
        c = Camera.main.GetComponent<cameraStuff>();
    }

    // Update is called once per frame
    void Update()
    {

        //if(!armHit.doing && transform.position != Vector3.zero) transform.position = Vector3.Lerp(transform.position, Vector3.zero,t);
        if(dead) return;
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 0 && !armHit.doing) GetComponent<Animator>().enabled = false;
        if (transform.parent.position != 3.5f*Vector3.up && !armHit.doing && reposT < 1) { transform.parent.position = Vector3.Lerp(transform.parent.position, Vector3.zero, Mathf.Pow(reposT,2)); reposT += 0.5f*Time.deltaTime; }
        t += Time.deltaTime;
        if(t > 2) 
        {
            t -= 2;
            GameObject a = attacks[Random.Range(0, attacks.Length)];
            Instantiate(attacks[1]);
            if(lives < 7 && lives > 3) Instantiate(attacks[1]);
            if (lives < 4) t++;
            abb++;
            if (abb > 5 && GameObject.FindGameObjectsWithTag("dynamite").Length == 0) Instantiate(dynamite, new Vector3(Random.Range(-6.8f, 5.7f), Random.Range(-5f,-2f), 0), Quaternion.identity);
            if(!bulletShot.doing && lives < 4)
            {
                int p = Random.Range(0, 4);
                if (p == 1) Instantiate(attacks[2]);
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
        if (lives == 0) StartCoroutine(End());
        Instantiate(explosionAnim, transform.position + new Vector3(Random.Range(-2,2), Random.Range(- 1,2)), Quaternion.identity);
        Instantiate(explosionAnim, transform.position + new Vector3(Random.Range(-2,2), Random.Range(- 1,2)), Quaternion.identity);
        Instantiate(explosionSound);

        StartCoroutine(c.cameraShake());
        
        StartCoroutine(Immortality());
    }
    IEnumerator Immortality()
    {
        foreach(SpriteRenderer s in sp) s.color -= new Color(0, 0, 0, 0.5f);

        yield return new WaitForSeconds(1);

        foreach (SpriteRenderer s in sp) s.color += new Color(0, 0, 0, 0.5f);
    }

    IEnumerator End()
    {
        float deadT = 0;
        float expT = 0;
        dead = true;
        while (deadT < 5)
        {
            deadT += Time.deltaTime;
            expT += Time.deltaTime;
            if(expT > 0.5f) { expT -= 0.5f; Instantiate(explosionAnim, transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-1, 2)), Quaternion.identity); Instantiate(explosionSound); }
            foreach (SpriteRenderer s in sp) s.color -= new Color(0, 0, 0, 0.2f*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Destroy(player);
        Destroy(music);
        theend.SetActive(true);


        Destroy(gameObject);
    }
}
