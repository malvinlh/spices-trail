using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject health;

    public void SetHP (float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f, 1f);
    }
}