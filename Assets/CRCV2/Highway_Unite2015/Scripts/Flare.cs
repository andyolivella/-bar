using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Flare : MonoBehaviour
{
    public float startRange; //your chosen start value

    public float endRange; //your chose end value

    public int speed;

    public bool startFirst;

    public virtual void Update()
    {
        float oscilationRange = (this.endRange - this.startRange) / 2;
        float oscilationOffset = oscilationRange + this.startRange;
        if (this.startFirst == true)
        {
            //gameObject.GetComponent(LensFlare).brightness = Mathf.Sin(Time.time * speed) * value;
            ((LensFlare) this.gameObject.GetComponent(typeof(LensFlare))).brightness = oscilationOffset + (Mathf.Sin(Time.time * this.speed) * oscilationRange);
        }
        else
        {
            if (this.startFirst == false)
            {
                //gameObject.GetComponent(LensFlare).brightness = Mathf.Sin(Time.time * speed) * -value;
                ((LensFlare) this.gameObject.GetComponent(typeof(LensFlare))).brightness = oscilationOffset + (Mathf.Sin(Time.time * this.speed) * -oscilationRange);
            }
        }
    }

    public Flare()
    {
        this.startRange = 10;
        this.endRange = 50;
        this.speed = 20;
        this.startFirst = true;
    }

}