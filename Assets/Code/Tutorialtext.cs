using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        // Start the fade-in process as soon as the game starts
        StartCoroutine(FadeTextToFullAlpha(5f, GetComponent<Text>()));
        
        // Optionally, start fading out after the fade-in is complete (you can adjust the delay or sequence timing)
        Invoke("FadeOut", 5f); // Call FadeOut method after 5 seconds
    }

    // This method will start the fade-out process after 5 seconds
    void FadeOut()
    {
        StartCoroutine(FadeTextToZeroAlpha(5f, GetComponent<Text>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0); // Start with alpha = 0
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1); // Start with alpha = 1
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}