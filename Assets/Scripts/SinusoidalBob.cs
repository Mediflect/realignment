using UnityEngine;

public class SinusoidalBob : MonoBehaviour
{
    public float period = 2f;
    public float amplitude = 0.2f;

    private float localY = 0f;
    private float timer = 0f;

    private void OnEnable()
    {
        localY = transform.localPosition.y;
    }

    private void OnDisable()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, localY, transform.localPosition.z);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > period)
        {
            timer -= period;
        }
        float t = Mathf.InverseLerp(0, period, timer);
        float sinPos = Mathf.Lerp(0, Mathf.PI * 2f, t);
        float yPos = localY + Mathf.Sin(sinPos) * amplitude;
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);    
    }
}
