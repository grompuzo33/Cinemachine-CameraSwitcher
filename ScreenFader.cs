using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    private static ScreenFader instance;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f; // ƒлительность затухани€ и по€влени€

    public static ScreenFader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<ScreenFader>(); 

                if (instance == null)
                {
                    Debug.LogError("The ScreenFader is not found");
                    return null;
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        if (fadeImage == null)
            Debug.LogError("Fade Image is not found");
    }

    
    public static void Fade(float fadeDuration, float blackScreenDuration)
    {
        if (Instance != null)
            Instance.StartFade(fadeDuration, blackScreenDuration);
    }

    
    public static void Fade(float blackScreenDuration)
    {
        if (Instance != null)
            Instance.StartFade(Instance.fadeDuration, blackScreenDuration);
    }

    
    public static void Fade()
    {
        if (Instance != null)
            Instance.StartFade(Instance.fadeDuration, 0f);
    }

    
    public static void Fade(float blackScreenDuration, System.Action onBlackScreenCallback)
    {
        if (Instance != null)
            Instance.StartFadeWithCallback(Instance.fadeDuration, blackScreenDuration, onBlackScreenCallback);
    }

    private void StartFade(float fadeDur, float blackScreenDur)
    {
        if (fadeImage == null) return;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(fadeDur, blackScreenDur, null));
    }

    private void StartFadeWithCallback(float fadeDur, float blackScreenDur, System.Action callback)
    {
        if (fadeImage == null) return;
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(fadeDur, blackScreenDur, callback));
    }

    private IEnumerator FadeRoutine(float fadeDur, float blackScreenDur, System.Action onBlackScreenCallback)
    {
        fadeImage.gameObject.SetActive(true);

        //Fade Out
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColorOut = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDur)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDur);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        fadeImage.color = targetColorOut;

        
        onBlackScreenCallback?.Invoke();

        
        yield return new WaitForSeconds(blackScreenDur);

        //Fade In
        elapsedTime = 0f;
        Color targetColorIn = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDur)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDur);
            fadeImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }
        fadeImage.color = targetColorIn;

        fadeImage.gameObject.SetActive(false);
    }

    // ƒополнительные статические методы дл€ контрол€

    // ѕринудительно установить затемнение 
    public static void SetFade(float alpha)
    {
        if (Instance != null && Instance.fadeImage != null)
        {
            Color color = Instance.fadeImage.color;
            Instance.fadeImage.color = new Color(color.r, color.g, color.b, Mathf.Clamp01(alpha));
            Instance.fadeImage.gameObject.SetActive(alpha > 0);
        }
    }

    
    public static void StopFade()
    {
        if (Instance != null)
        {
            Instance.StopAllCoroutines();
            if (Instance.fadeImage != null)
                Instance.fadeImage.gameObject.SetActive(false);
        }
    }

   
    public static bool IsFading()
    {
        return Instance != null && Instance.fadeImage != null &&
               Instance.fadeImage.gameObject.activeInHierarchy;
    }

    
    public static float GetCurrentAlpha()
    {
        if (Instance != null && Instance.fadeImage != null)
            return Instance.fadeImage.color.a;
        return 0f;
    }
}