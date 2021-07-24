using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
[RequireComponent(typeof(CanvasGroup))]
public class BlurPanel : Image
{
    [SerializeField] private float time = 0.5f;
    [SerializeField] private float delay = 0f;
    [SerializeField] private int step = 1;
    [SerializeField] private bool animate;

    private CanvasGroup canvGroup = default;

    private WaitForEndOfFrame waitForFrame = new WaitForEndOfFrame();
    private static readonly int Size = Shader.PropertyToID("_Size");

    protected override void Awake()
    {
        base.Awake();

        canvGroup = GetComponent<CanvasGroup>();
    }
    
    protected override void OnEnable()
    {
        if (Application.isPlaying)
        {
            material.SetFloat(Size, 0);
            canvGroup.alpha = 0;
            StartCoroutine(Tween(0, 1));
        }
    }

    private IEnumerator Tween(int a, int b)
    {
        for (int i = a; i < b; i += step)
        {
            material.SetFloat(Size, (float)b / i);
            yield return waitForFrame;
        }
    }
}
