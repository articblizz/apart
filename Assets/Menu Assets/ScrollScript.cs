using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {


    RectTransform rTransform;
    public float maxBottom = -3;

    float originalY;


    // Use this for initialization
    void Start () {
        rTransform = GetComponent<RectTransform>();

        originalY = rTransform.position.y;
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void Scroll(float value)
    {
        //Debug.Log(value);
        var pos = rTransform.position;
        pos.y = originalY + (maxBottom * value);
        rTransform.position = pos;
    }
}
