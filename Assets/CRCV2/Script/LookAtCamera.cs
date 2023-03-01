using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class LookAtCamera : MonoBehaviour
{
    private GameObject select;

    public virtual void Update()// gameObject.SetActive (false);  // Unite 2015
    {
        this.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
    }

}