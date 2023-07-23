using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }


    IEnumerator FadeIn ()
    {
        float time = 1.0f;

        while (time > 0.0f)
        {
            
            time -= Time.deltaTime;
            float alpha = curve.Evaluate(time);

            img.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return 0;
        }
    }

    public void FadeTo(string scene)
    {
        StartCoroutine (FadeOut(scene));
    }

    IEnumerator FadeOut(string scene)
    {
        float time = 0.0f;

        while (time < 1.0f)
        {

            time += Time.deltaTime;
            float alpha = curve.Evaluate(time);

            img.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }


}
