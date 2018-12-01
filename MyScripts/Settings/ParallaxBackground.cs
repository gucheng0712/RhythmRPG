using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("=====Drag into BGs from Near to Far=====")]
    [SerializeField] Transform[] bgs;

    [Space(10)]
    [Header("=====The Scale of Parallax =====")]
    [RangeAttribute(0,-10)]
    public float parallaxScale0 = 0f;
    [RangeAttribute(0, -10)]
    public float parallaxScale1 = 0f;
    [RangeAttribute(0, -10)]
    public float parallaxScale2 = 0f;
    [RangeAttribute(0, -10)]
    public float parallaxScale3 = 0f;
    [RangeAttribute(0, -10)]
    public float parallaxScale4 = 0f;
    [HideInInspector]
    public float bgTargetPosX;

    Transform cam;

    Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void Update()
    {
        float parallax0 = (previousCamPos.x - cam.position.x) * parallaxScale0;
        float parallax1 = (previousCamPos.x - cam.position.x) * parallaxScale1;
        float parallax2 = (previousCamPos.x - cam.position.x) * parallaxScale2;
        float parallax3 = (previousCamPos.x - cam.position.x) * parallaxScale3;
        float parallax4 = (previousCamPos.x - cam.position.x) * parallaxScale4;

        for (int i = 0; i < bgs.Length; i++)
        {
            switch (i)
            {
                case 0:
                    bgTargetPosX = bgs[i].position.x + parallax0;
                    break;
                case 1:
                    bgTargetPosX = bgs[i].position.x + parallax1;
                    break;
                case 2:
                    bgTargetPosX = bgs[i].position.x + parallax2;
                    break;
                case 3:
                    bgTargetPosX = bgs[i].position.x + parallax3;
                    break;
                case 4:
                    bgTargetPosX = bgs[i].position.x + parallax4;
                    break;
                default:
                    break;
            }
                Vector3 bgTargetPos = new Vector3(bgTargetPosX, bgs[i].position.y, bgs[i].position.z);
                bgs[i].position = Vector3.Lerp(bgs[i].position, bgTargetPos, 10.0f * Time.deltaTime);
            //float bgTargetPosX = bgs[i].position.x + parallax * i;
            //Vector3 bgTargetPos = new Vector3(bgTargetPosX, bgs[i].position.y, bgs[i].position.z);
            //bgs[i].position = Vector3.Lerp(bgs[i].position, bgTargetPos, 10.0f * Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
