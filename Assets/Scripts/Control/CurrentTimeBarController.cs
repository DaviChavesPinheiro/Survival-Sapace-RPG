using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTimeBarController : MonoBehaviour
{
    [SerializeField] Renderer fillBar;
    Material fill;
    private void Awake() {
        fill = fillBar.material;
    }

    public void SetOffset(float offSet){
        fill.SetTextureOffset ("_MainTex", new Vector2(offSet, 0));
    }
}
