using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;
using System.Collections;
public class movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    SpriteRenderer sp;
    BoxCollider2D bc;
    [SerializeField] private monster m;
    cameraStuff c;
    float speed = 5;
    bool dynamite = false;
    int lives = 3;
    Transform inventorySlot;
    [SerializeField] private SpriteRenderer hitBG;

    Vector2 prevInput = Vector3.one;
    //cameraStuff c;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        c = Camera.main.GetComponent<cameraStuff>();
        //c = Camera.main.GetComponent<cameraStuff>();
        inventorySlot = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (hitBG.color.a > 0) hitBG.color -= new Color(0, 0, 0,2* Time.deltaTime);
        //handleInput();
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(input != prevInput)
        {
            if (input == Vector2.zero)
            {
                an.Play(an.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
                an.speed = 0;
            }
            else
            {
                an.speed = 1;
                if (input.y < -0.707f)
                {
                    an.SetInteger("state", 0);
                    inventorySlot.transform.localPosition = new Vector2(0.1f, -0.1f);
                    inventorySlot.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
                else if (input.y > 0.707f) 
                {
                    an.SetInteger("state", 1);
                    inventorySlot.transform.localPosition = new Vector2(-0.1f, -0.1f);
                    inventorySlot.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                else if (input.x > 0.707f)
                {
                    an.SetInteger("state", 2);
                    sp.flipX = false;
                    inventorySlot.transform.localPosition = new Vector2(0.1f, -0.1f);
                    inventorySlot.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                else if (input.x < -0.707f)
                {
                    an.SetInteger("state", 2);
                    sp.flipX = true;
                    inventorySlot.transform.localPosition = new Vector2(-0.1f, -0.1f);
                    inventorySlot.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        rb.linearVelocity = input*speed;
        prevInput = input;
    }

    //int handleInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.A)) inputs.Add(2);
    //    else if (Input.GetKeyDown(KeyCode.A)) inputs.Remove(2);

    //    if (Input.GetKeyDown(KeyCode.D)) inputs.Add(2);
    //    else if (Input.GetKeyDown(KeyCode.D)) inputs.Remove(2);

    //    if (Input.GetKeyDown(KeyCode.W)) inputs.Add(1);
    //    else if (Input.GetKeyDown(KeyCode.W)) inputs.Remove(1);

    //    else if (Input.GetKeyDown(KeyCode.S)) inputs.Add(0);
    //    return inputs[^1];
    //}
    //int handleInput()
    //{
    //    if (Input.GetKeyDown(KeyCode.A)) inputs.Add(2);
    //    else if (Input.GetKeyDown(KeyCode.A)) inputs.Remove(2);

    //    if (Input.GetKeyDown(KeyCode.D)) inputs.Add(2);
    //    else if (Input.GetKeyDown(KeyCode.D)) inputs.Remove(2);

    //    if (Input.GetKeyDown(KeyCode.W)) inputs.Add(1);
    //    else if (Input.GetKeyDown(KeyCode.W)) inputs.Remove(1);

    //    else if (Input.GetKeyDown(KeyCode.S)) inputs.Add(0);
    //    return inputs[0];
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dynamite") && Input.GetKey(KeyCode.Space) && !dynamite) { Destroy(collision.gameObject); dynamite = true; inventorySlot.gameObject.SetActive(true); }
        else if (collision.gameObject.CompareTag("explosion") && Input.GetKey(KeyCode.Space) && dynamite) { m.hit(); dynamite = false; inventorySlot.gameObject.SetActive(false); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hit")) hit();
        if (collision.gameObject.name == "Line") StartCoroutine(c.transition(new Vector3(0, -12, -10)));
    }

    void hit()
    {
        lives--;
        if (lives == 0) Destroy(gameObject);
        hitBG.color += new Color(0, 0, 0, 0.5f);
        StartCoroutine(Immortality());
    }

    IEnumerator Immortality()
    {
        StartCoroutine(c.cameraShake());
        bc.enabled = false;
        sp.color -= new Color(0, 0, 0, 0.5f);
        
        yield return new WaitForSeconds(1);
        bc.enabled = true;
        sp.color += new Color(0, 0, 0, 0.5f);
    }
}
