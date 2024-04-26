using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;
using PathCreation;

public class Sun : MonoBehaviour
{
    public Light2D sunLight;

    [Header("曲线部分")]
    public AnimationCurve curve;
    public AnimationCurve StarNumCurve;

    [Header("粒子部分")]
    public ParticleSystem[] Stars;
    public ParticleSystem Cloud;

    [Tooltip("最大粒子数")]
    public int MaxStarNum;

    public Transform MainCamera;

    public EndOfPathInstruction endOfPathInstruction;

    public float speed = 5;

    private float horizonHeight;

    private float maxX;

    private float minX;

    float distanceTravelled;

    private Vector3 offset;

    public PathCreator path;

    private void Start()
    {
        offset = MainCamera.position - path.transform.position;

        if (path != null)
        {
            path.pathUpdated +=OnPathChanged;

            horizonHeight = path.bezierPath.PathBounds.center.y;

            maxX = path.bezierPath.PathBounds.max.x;
            minX= path.bezierPath.PathBounds.min.x;
        }
    }

    private void Update()
    {
        if (transform.position.y>horizonHeight)
        {
            float sunPos = (transform.position.x - path.transform.position.x - minX) / (maxX - minX);
            //Debug.Log(sunPos);
            sunLight.intensity = curve.Evaluate(sunPos);
            for (int i = 0; i < Stars.Length; i++)
            {
                MainModule main = Stars[i].main;
                main.maxParticles = Mathf.Max(0, (int)(MaxStarNum * StarNumCurve.Evaluate(sunPos)));
        }
        }
        else
        {
            sunLight.intensity = 0.08f;
            for (int i = 0; i < Stars.Length; i++)
            {
                MainModule main = Stars[i].main;
                main.maxParticles = MaxStarNum;
            }
        }

        if (path!=null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

            Follow();
        }     
    }

    void Follow()
    {
        path.transform.position = new Vector3(MainCamera.position.x + offset.x, path.transform.position.y, 0);//只会变化x保持摄像机运动;
    }

    public void OnPathChanged()
    {
        distanceTravelled = path.path.GetClosestDistanceAlongPath(transform.position);
    }
}
