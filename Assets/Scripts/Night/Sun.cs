using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.ParticleSystem;
using PathCreation;
using System;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    public Light2D sunLight;

    public float weekPoint;
    public float dayPoint;

    public float light_Intensity_DayTime;
    public float light_Intensity_WeekTime;

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

    bool flag;

    private void Start()
    {
        // offset = MainCamera.position - path.transform.position;

        if (path != null)
        {
            //gameObject.transform.position= WeekPoint.position; 
            path.pathUpdated += OnPathChanged;

            horizonHeight = path.bezierPath.PathBounds.center.y;

            maxX = path.bezierPath.PathBounds.max.x;
            minX= path.bezierPath.PathBounds.min.x;
        }

        
    }

    void OnEnable()
    {
        // TimeEventSystem.onTimeChanging += OnTimeChanging;
    }

    void OnDisable()
    {
        // TimeEventSystem.onTimeChanging -= OnTimeChanging;
    }

    private void Update()
    {
        if (TimeEventSystem.instance.timeQuantum == TimeQuantum.DayTime)
        {
            if (Math.Abs(distanceTravelled - dayPoint) > 0.2f)
            {
                if (path != null)
                {
                    distanceTravelled += speed * Time.deltaTime * 1.2f;
                    distanceTravelled %= path.path.length;
                    transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    Follow();
                }
            }
            else
            {
                
                //distanceTravelled = 0;
                transform.position = path.path.GetPointAtDistance(dayPoint, endOfPathInstruction);
            }
        }

        if (TimeEventSystem.instance.timeQuantum == TimeQuantum.WeekHours)
        {
            Debug.Log(distanceTravelled);
            if (Math.Abs(distanceTravelled - weekPoint) > 0.2f)
            {
                if (path != null)
                {
                    distanceTravelled += speed * Time.deltaTime;
                    distanceTravelled %= path.path.length;
                    transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    Follow();
                    
                }
            }
            else
            {
                // distanceTravelled = 0;
                transform.position = path.path.GetPointAtDistance(weekPoint, endOfPathInstruction);
            }
        }   
    
        if(distanceTravelled > 20f)
        {
            WeekDay();
        }
        else
        {
            Daytime();
        }
    }


    float timeDef = 0;
    void Follow()
    {
        path.transform.position = new Vector3(MainCamera.position.x + offset.x, path.transform.position.y, 0);//只会变化x保持摄像机运动;
    }

    public void OnPathChanged()
    {
        distanceTravelled = path.path.GetClosestDistanceAlongPath(transform.position);
    }

    void OnTimeChanging()
    {
        Debug.Log($"Sun_onLastTimeEnd");
        timeDef = 0;
    }

    void Daytime()
    {
        if (transform.position.y > horizonHeight)
        {
            float sunPos = (transform.position.x - path.transform.position.x - minX) / (maxX - minX);
            DOTween.To(()=>sunLight.intensity, x => sunLight.intensity = x, light_Intensity_DayTime, 4f);


            for (int i = 0; i < Stars.Length; i++)
            {
                MainModule main = Stars[i].main;
                main.maxParticles = 0;
            }

            // 计算云的颜色，根据您希望的变化速度进行调整
            float cloudColorProgress = Mathf.Clamp01(sunPos * 2f); // 假设变化速度为太阳位置的两倍
            Color cloudColor = Color.Lerp(new Color(0.89f, 0.89f, 0.89f), Color.white, cloudColorProgress);
            MainModule mainModule = Cloud.main;
            mainModule.startColor = cloudColor;
        }
    }

    void WeekDay()
    {
        // if(timeDef < 5f)
        // {
        //     float def = timeDef / 5;
        //     sunLight.intensity = Mathf.Lerp(sunLight.intensity, light_Intensity_WeekTime, def);
        //     timeDef += Time.deltaTime;
        // }
        DOTween.To(()=>sunLight.intensity, x => sunLight.intensity = x, light_Intensity_WeekTime, 4f);
        
        for (int i = 0; i < Stars.Length; i++)
        {
            MainModule main = Stars[i].main;
            main.maxParticles = MaxStarNum;
        }
        MainModule mainModule = Cloud.main;
        mainModule.startColor = Color.Lerp(new Color(0.89f, 0.89f, 0.89f), Color.white, 0.5f);
    }
}
