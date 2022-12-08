using UnityEngine;

public class PaddleGrowth : MonoBehaviour
{
    private float secondsLeft = 0;
    private Vector3 normalScale = Vector3.one * .7f;
    private Vector3 grownScale = Vector3.one * 2;
    [SerializeField] private ParticleSystem growingEffect;
    [SerializeField] private GameObject shrinkingEffect;
    private float secondsPlayed = 0;

    public void powerup(float seconds)
    {
        secondsLeft = seconds;
        growingEffect.Play();
        secondsPlayed = 0;
    }

    private void Update()
    {
        secondsPlayed += Time.deltaTime;
        if (secondsPlayed > 3.1f)
        {
            growingEffect.Stop();
        }
        if (secondsLeft > 0)
        {
            secondsLeft -= Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, grownScale, Time.deltaTime);
        } else
        {
            if (this.transform.localScale.x - normalScale.x > .1)
            {
                shrinkingEffect.SetActive(true);
            } else
            {
                shrinkingEffect.SetActive(false);
            }
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, normalScale, Time.deltaTime);
        }
    }
}
