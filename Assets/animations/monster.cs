using UnityEngine;

public class monster : MonoBehaviour
{
    float t = 0;
    [SerializeField] private GameObject[] attacks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 2) { t -= 2; Instantiate(attacks[Random.Range(0,attacks.Length)]); Instantiate(attacks[Random.Range(0, attacks.Length)]); }
    }
}
