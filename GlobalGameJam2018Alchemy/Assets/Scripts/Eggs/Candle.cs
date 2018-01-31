using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public Light lightSource;
    private void OnMouseDown() => lightSource.enabled = !lightSource.isActiveAndEnabled;
}
