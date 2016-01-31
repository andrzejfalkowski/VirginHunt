using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Sky : MonoBehaviour {

    private float morningDuration = Globals.DAY_DURATION * 0.2f;
    private float dayDuration = Globals.DAY_DURATION * 0.5f;
    private float eveningDuration = Globals.DAY_DURATION * 0.3f;
    private float nightDuration = Globals.NIGHT_DURATION;

    public SpriteRenderer MorningSkyRenderer;
    public SpriteRenderer DaySkyRenderer;
    public SpriteRenderer EveningSkyRenderer;
    public SpriteRenderer NightSkyRenderer;

    private float currentDayTime = 0f;

    private float TransitionTime = Globals.DAY_DURATION/10;
    private float FadingAlpha;

    public enum EDayPhase
    {
        Morning,
        Day,
        Evening,
        Night
    }
    public EDayPhase CurrentGamePhase = EDayPhase.Morning;


    // Update is called once per frame
    void Update()
    {
        currentDayTime += Time.deltaTime;

        switch (CurrentGamePhase)
        {
            case EDayPhase.Morning:
                if (currentDayTime > morningDuration)
                {
                    currentDayTime = currentDayTime % morningDuration;
                    SkyTransition(MorningSkyRenderer, DaySkyRenderer);
                    CurrentGamePhase = EDayPhase.Day;
                }
                break;
            case EDayPhase.Day:
                if (currentDayTime > dayDuration)
                {
                    currentDayTime = currentDayTime % dayDuration;
                    SkyTransition(DaySkyRenderer, EveningSkyRenderer);
                    CurrentGamePhase = EDayPhase.Evening;
                }
                break;
            case EDayPhase.Evening:
                if (currentDayTime > eveningDuration)
                {
                    currentDayTime = currentDayTime % eveningDuration;
                    SkyTransition(EveningSkyRenderer, NightSkyRenderer);
                    CurrentGamePhase = EDayPhase.Night;
                }
                break;
            case EDayPhase.Night:
                if (currentDayTime > nightDuration)
                {
                    currentDayTime = currentDayTime % nightDuration;
                    SkyTransition(NightSkyRenderer, MorningSkyRenderer);
                    CurrentGamePhase = EDayPhase.Morning;
                }
                break;
        }
    }
    

    void SkyTransition(SpriteRenderer DissaperingSky, SpriteRenderer ApperingSky)
    {
        DOTween.To(() => DissaperingSky.color, (colMorning) => DissaperingSky.color = colMorning, new Color(255, 255, 255, 0), 5f);
        DOTween.To(() => ApperingSky.color, (colDay) => ApperingSky.color = colDay, new Color(255, 255, 255, 1), 5f);
    }
}
