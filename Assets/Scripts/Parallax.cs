using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float backgroundLength;//圖的長度
    private float startpos;
    public float parallaxEffect;

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        backgroundLength = GetComponent<SpriteRenderer>().bounds.size.x;

    }
    private void FixedUpdate()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float dist = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + backgroundLength)
        {
            startpos += backgroundLength;
        }
        else if (temp<startpos-backgroundLength)
        {
            startpos -= backgroundLength;
        }
    }
}
