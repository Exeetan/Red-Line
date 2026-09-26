using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class pressR : MonoBehaviour
{
    monster m;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m = GameObject.FindGameObjectWithTag("monster").transform.GetChild(0).GetComponent<monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.SetActive(true);
            player.GetComponent<movement>().enabled = true;
            player.transform.position = -4.5f * Vector3.up;
            player.GetComponent<movement>().lives = 3;
            m.lives = 8;
            m.transform.position = m.transform.parent.position = 3.5f * Vector3.up;
            m.GetComponent<Animator>().enabled = false;
            foreach(debrisPunch d in FindObjectsByType<debrisPunch>()) Destroy(d.gameObject);
            foreach(armHit d in FindObjectsByType<armHit>()) Destroy(d.gameObject);
            foreach(bulletShot d in FindObjectsByType<bulletShot>()) Destroy(d.gameObject);
            foreach(autoDestroy d in FindObjectsByType<autoDestroy>()) Destroy(d.gameObject);
            music.GetComponent<AudioSource>().Stop();
            music.GetComponent<AudioSource>().time = 0f;
            music.GetComponent<AudioSource>().Play();
            explosion.GetComponent<CircleCollider2D>().enabled = true;
            explosion.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.816f, 0.32f, 0.32f, 1);
            StopAllCoroutines();
            player.GetComponent<movement>().dynamite = false;
            gameObject.SetActive(false);
        }
    }
}
