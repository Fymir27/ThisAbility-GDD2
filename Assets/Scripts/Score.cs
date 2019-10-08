using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public struct EndscreenData
    {
        public float bonusObjectivesCollected;
        public float secondsInLevel;
        public float timesRestarted;
        public float score;
    }

    public EndscreenData data = new EndscreenData();

    void Awake()
    {
        Instance = this;
        LevelController.levelChanged += OnLevelChange;
    }

    void Update()
    {
        data.secondsInLevel += Time.deltaTime;
    }

    void OnLevelChange(LevelController.Levels useless)
    {
        data.bonusObjectivesCollected = 0;
        data.secondsInLevel = 0;
    }
    public void IncrementBonusObjective()
    {
        data.bonusObjectivesCollected++;
    }
    public void IncrementTimesRestarted()
    {
        data.timesRestarted++;
    }
    public void IncrementScore()
    {
        data.score++;
    }
    public EndscreenData CurrentLevelData()
    {
        return data;
    }

    public void ResetBonusObjective()
    {
        data.bonusObjectivesCollected = 0;
    }

    public void CalculateScore()
    {
        float fromTime = 100f / Mathf.Sqrt(data.secondsInLevel); // improve based on expected level time
        float fromBonus = 100f * data.bonusObjectivesCollected;
        float fromRestarts = Mathf.Max(0, 10 - data.timesRestarted) * 10f;

        data.score = fromTime + fromBonus + fromRestarts;
    }
}
