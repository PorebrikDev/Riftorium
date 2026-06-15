using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DelayedAbility : MonoBehaviour
{
    private Image _image;
    private Coroutine _coroutine;
    private Color _baseColor;
    private Color _delayColor = new Color(0.8f, 0.8f, 0.4f, 1f);

    private void Awake()
    {
        _image = GetComponent<Image>();
        _baseColor = _image.color;
        _image.fillAmount = 1;
    }

    public void StartFilled(float duration)

    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(FillRoutine(duration));

    }

    private IEnumerator FillRoutine(float duration)
    {
        float timer = 0f;
        _image.color = _delayColor;
        _image.fillAmount = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            _image.fillAmount = timer / duration;
            yield return null;
        }
        _image.color = _baseColor;
        _image.fillAmount = 1f;
        _coroutine = null;
    }
}
