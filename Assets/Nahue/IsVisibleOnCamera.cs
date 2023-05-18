using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisibleOnCamera : MonoBehaviour
{
    public bool visible = false;

    private void OnBecameInvisible()
    {
        visible = false;
    }

    private void OnBecameVisible()
    {
        visible = true;
    }
}
