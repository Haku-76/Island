using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = this.transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        this.transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
