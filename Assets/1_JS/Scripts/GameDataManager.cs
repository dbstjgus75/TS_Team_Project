using UnityEngine;

public class GameDataManager
{
    public static GameDataManager aInstance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameDataManager();
            }
            return sInstance;
        }
    }
    public void Init()
    {

    }
    public void Clear()
    {

    }
    
    private static GameDataManager sInstance = null;
}
