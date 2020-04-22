using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    public float targetScale;   // Final scale
    public float timeToReachTarget; // Time to reach the target scale
    private float startScale; 
    private float percentScaled; 

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (percentScaled < 1f) 
        {
            percentScaled += Time.deltaTime / timeToReachTarget; 
            float scale = Mathf.Lerp(startScale, targetScale, percentScaled); 
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
