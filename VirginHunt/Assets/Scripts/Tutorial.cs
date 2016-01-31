using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class Tutorial : MonoBehaviour {

	private float tutorialTime = 0f;
    private float secondTutorialTime = 6f;
    private float thirdTutorialTime = 12f;
    private float fourthTutorialTime = 18f;
    private float fifthTutorialTime = 24f;
    private float endTutorialTime = 30f;
    private float tweeningDelay = 2f;

    public Text TutorialText;

    #region Singleton
    private static Tutorial _instance = null;

    public static Tutorial Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    void Start () {
	    
	}

    void Update()
    {
        tutorialTime = tutorialTime + Time.deltaTime;
        Debug.Log(tutorialTime);
        if(tutorialTime > endTutorialTime)
        {
            EndTutorial();
        }
        else if (tutorialTime > fifthTutorialTime)
        {
            ChangeTutorialText(5);
        }
        else if(tutorialTime > fourthTutorialTime)
        {
            ChangeTutorialText(4);
        }
        else if (tutorialTime > thirdTutorialTime)
        {
            ChangeTutorialText(3);
        }
        else if (tutorialTime > secondTutorialTime)
        {
            ChangeTutorialText(2);
        }
    }

    void ChangeTutorialText(int chosenText)
    {
        switch (chosenText)
        {
            case 1:
                TutorialText.text = "Power on altar will defend village";
                break;
            case 2:
                TutorialText.text = "Powered altar can defend village";
                break;
            case 3:
                TutorialText.text = "Order villager to pray or sacriface them";
                break;
            case 4:
                TutorialText.text = "Use space to pick them up";
                break;
            case 5:
                TutorialText.text = "Look inside their minds, find virgins!";
                break;
        }
    }

    void EndTutorial()
    {
        DOTween.To(() => TutorialText.color, (col) => TutorialText.color = col, new Color(0, 0, 0, 0), 2f);
    }

}
	
