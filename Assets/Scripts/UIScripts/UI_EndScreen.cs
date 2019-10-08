using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_EndScreen : UIPanel
{

    Animator animator;
    [SerializeField]
    TextMeshProUGUI timeTaken;
    [SerializeField]
    TextMeshProUGUI peopleSaved;
    [SerializeField]
    TextMeshProUGUI timesRestarted;
    [SerializeField]
    TextMeshProUGUI score;

    Score.EndscreenData data;

    void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        Score.Instance.CalculateScore();
        data = Score.Instance.CurrentLevelData();
        timeTaken.SetText(Mathf.RoundToInt(data.secondsInLevel).ToString() + " s");
        peopleSaved.SetText(Mathf.RoundToInt(data.bonusObjectivesCollected).ToString());
        timesRestarted.SetText(Mathf.RoundToInt(data.timesRestarted).ToString());
        score.SetText(Mathf.RoundToInt(data.score).ToString());

        base.Activate();
        animator.SetTrigger("Start");

    }
    public override void Deactivate()
    {
        base.Deactivate();
    }
}
