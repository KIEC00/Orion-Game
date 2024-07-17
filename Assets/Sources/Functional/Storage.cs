using UnityEngine;

public static class Storage
{
    const string HIGH_SCORE_KEY = "HS";

    public static int HighScore
    {
        get => PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        set => PlayerPrefs.SetInt(HIGH_SCORE_KEY, value);
    }
}
