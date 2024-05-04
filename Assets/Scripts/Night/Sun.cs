using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;
using PathCreation;
using System;

public class Sun : MonoBehaviour
{
    public Light2D sunLight;
    public Transform WeekPoint;
    public Transform DayPoint;

    [Header("曲线部分")]
    public AnimationCurve curve;
    public AnimationCurve StarNumCurve;

    [Header("粒子部分")]
    public ParticleSystem[] Stars;
    public ParticleSystem Cloud;

    [Tooltip("最大粒子数")]
    public int MaxStarNum;

    public Transform Sprite;

    public EndOfPathInstruction endOfPathInstruction;

    public float speed = 10;

    private float horizonHeight;

    private float maxX;

    private float minX;

    float distanceTravelled;

    private Vector3 offset;

    public PathCreator path;


    void Awake()
    {
        sunLight = FindAnyObjectByType<Light2D>();
    }

    private void Start()
    {
        offset = Sprite.position - path.transform.position;

        if (path != null)
        {
            //gameObject.transform.position= WeekPoint.position; 
            path.pathUpdated +=OnPathChanged;

            horizonHeight = path.bezierPath.PathBounds.center.y;

            maxX = path.bezierPath.PathBounds.max.x;
            minX= path.bezierPath.PathBounds.min.x;
        }
    }

    private void Update()
    {
        if (TimeEventSystem.instance.timeQuantum == TimeQuantum.DayTime)
        {
            if (transform.position.x<DayPoint.position.x)
            {
                if (path != null)
                {
                    distanceTravelled += speed * Time.deltaTime * 1.2f;
                    transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    Follow();
                    Daytime();
                }
            }
            else
            {
                //distanceTravelled = 0;
                transform.position = DayPoint.position;
            }
        }

        if (TimeEventSystem.instance.timeQuantum == TimeQuantum.WeekHours)
        {
            if (transform.position.y > WeekPoint.position.y)
            {
                if (path != null)
                {
                    distanceTravelled += speed * Time.deltaTime * 1.2f;
                    transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    Follow();
                    Daytime();
                }
            }
            else
            {
                distanceTravelled = 0;
                transform.position = WeekPoint.position;
            }
        }
    }


    void Follow()
    {
        path.transform.position = new Vector3(Sprite.position.x + offset.x, path.transform.position.y, 0);//只会变化x保持摄像机运动;
    }

    public void OnPathChanged()
    {
        distanceTravelled = path.path.GetClosestDistanceAlongPath(transform.position);
    }

    void Daytime()
    {
        if (transform.position.y > horizonHeight)
        {

            float sunPos = (transform.position.x - path.transform.position.x - minX) / (maxX - minX);
            sunLight.intensity = curve.Evaluate(sunPos);

            for (int i = 0; i < Stars.Length; i++)
            {
                MainModule main = Stars[i].main;
                main.maxParticles = Mathf.Max(0, (int)(MaxStarNum * StarNumCurve.Evaluate(sunPos)));
            }

            // 计算云的颜色，根据您希望的变化速度进行调整
            float cloudColorProgress = Mathf.Clamp01(sunPos * 2f); // 假设变化速度为太阳位置的两倍
            Color cloudColor = Color.Lerp(new Color(0.89f, 0.89f, 0.89f), Color.white, cloudColorProgress);
            MainModule mainModule = Cloud.main;
            mainModule.startColor = cloudColor;
        }
        else
        {
            WeekDay();
        }
    }

    void WeekDay()
    {
        sunLight.intensity = 0.08f;
        for (int i = 0; i < Stars.Length; i++)
        {
            MainModule main = Stars[i].main;
            main.maxParticles = MaxStarNum;
        }
        MainModule mainModule = Cloud.main;
        mainModule.startColor = Color.Lerp(new Color(0.89f, 0.89f, 0.89f), Color.white, 0.5f);
    }
}
