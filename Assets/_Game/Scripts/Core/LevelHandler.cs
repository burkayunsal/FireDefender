using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] Level testLevel;
    [SerializeField] Transform pool;
    [SerializeField] Level[] allLevels;

    Level crntLevel;

    public Level GetLevel() => crntLevel;

    private void Start()
    {
        CreateLevel();

        TouchHandler.I.Initialize(TouchHandler.I.useJoystick ? TouchHandler.TouchTypes.Joystick : TouchHandler.TouchTypes.Core, isButtonDerived: true, isStart: true);
        
        GameManager.OnStartGame();

    }

    public void CreateLevel()
    {
        if (testLevel == null && allLevels.Length == 0) return;

        int levelID = allLevels.Length >= 1 ? SaveLoadManager.GetLevel() % allLevels.Length : 0;

        crntLevel = Instantiate(testLevel != null ? testLevel : allLevels[levelID], pool);

        GameManager.canStart = true;
    }

}
