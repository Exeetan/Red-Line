using UnityEngine;

public class transition : MonoBehaviour
{
    cameraStuff c;
    private void Start()
    {
        c = Camera.main.GetComponent<cameraStuff>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) ;
    }

}
