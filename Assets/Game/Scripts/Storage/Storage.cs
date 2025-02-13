using UnityEngine;

public static class Storage
{
    private const string LEVEL = "level";
    private const string DESTROYED_BRICKS = "dbricks";

    public static void SaveLastLevel(int index)
    {
        PlayerPrefs.SetInt(LEVEL, index);
        PlayerPrefs.Save();
    }

    public static void AddDestroyedBrick()
    {
        var bricks = PlayerPrefs.GetInt(DESTROYED_BRICKS);
        PlayerPrefs.SetInt(DESTROYED_BRICKS, bricks + 1);
    }

    public static int GetLastSavedLevel()
    {
        //if (!PlayerPrefs.HasKey(LEVEL))
            //return 0;

        return PlayerPrefs.GetInt(LEVEL);
    }

    public static int GetDestroyedBricks()
    {
        return PlayerPrefs.GetInt(DESTROYED_BRICKS);
    }
}
