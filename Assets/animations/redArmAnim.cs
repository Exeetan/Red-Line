using UnityEngine;

public class redArmAnim : MonoBehaviour
{
    float t = 0;
    void Update()
    {
        t += Time.deltaTime;
        if(t > 2*Mathf.PI) t -= 2*Mathf.PI;

        transform.GetChild(1).GetChild(0).eulerAngles = 5 * Mathf.Sin(t) * Vector3.forward;
        transform.GetChild(1).GetChild(0).GetChild(1).eulerAngles = 10 * Mathf.Sin(t) * Vector3.forward;
        transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).eulerAngles = 15 * Mathf.Sin(t) * Vector3.forward;
    }
}
