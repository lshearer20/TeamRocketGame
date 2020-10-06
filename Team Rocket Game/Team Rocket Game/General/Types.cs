namespace Team_Rocket_Game
{
    //Settings, factories, and game states
    public enum EnemyType
    {
        EnemyA, EnemyB, MidBoss, FinalBoss
    }

    public enum BulletType
    {
        Player, EnemyA, EnemyB, MidBoss, FinalBoss
    }

    public enum Difficulty
    {
        Easy, Medium, Hard, Insane
    }

    public enum MoveKeys
    {
        ARROW, WASD
    }

    public enum MovementPattern
    {
        Straight, Oscillate, None
    }

    public enum GameOver
    {
        WIN, LOSE
    }

    public enum ScreenView
    {
        DIFFICULTY_SETTINGS, SETTINGS, INGAME, GAMEWIN, GAMELOSE, MENU, PAUSED_MENU
    }

    public enum GameState
    {
        PLAY, GAMEOVER, MENU, EXIT, RESET, PAUSE, PAUSEDMENU
    }

    public enum MenuOption
    {
        ARROWKEYS, WASDKEYS, DIFFICULTY, MENU, PLAY_GAME, SETTINGS, EXIT
    }

    public enum DifficultyMenuOption
    {
        MENU, EASY, MEDIUM, HARD, INSANE
    }
}