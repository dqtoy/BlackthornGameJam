using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class FadeInOut : MonoBehaviour
{

    public Image BG;
    public Text Text;
    public float valueFrom;
    public float valueTo;
    public float time = 1.5f;
    public float delay;
    public Ease easeType = Ease.Linear;

    public UnityEvent callback;

    private bool playing = false;
    private float progress;

    private void Awake()
    {
        if (callback == null)
            callback = new UnityEvent();
    }

    private void Update()
    {
        if (!playing)
            return;

        if (BG != null)
            BG.color = new Color(BG.color.r, BG.color.g, BG.color.b, progress);
        if (Text != null)
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, progress);
    }

    public void Play()
    {
        progress = valueFrom;
        DOTween.To(() => progress, x => progress = x, valueTo, time).SetDelay(delay).SetEase(easeType).OnComplete(OnFinish);

        playing = true;
    }

    public void OnFinish()
    {
        playing = false;
        callback.Invoke();
    }
}