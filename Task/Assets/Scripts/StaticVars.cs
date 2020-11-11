public static class StaticVars
{
    //border limits
    private static float northEdgeLimit = 10.5f;
    private static float eastEdgeLimit = 14.5f;

    //enemy
    private static int enemyHealth = 5;
    private static float enemyDetectionRange = 6.5f;
    private static float enemyAttackRange = 3;
    private static float enemyDetectedSpeed = 2.5f;
    private static float enemyRandomSpeed = 2f;


    //wave
    private static int firstWaveAmount = 5;
    private static int waveIncrement = 1;

    //player
    private static int playerHealth = 10;
    private static float cooldown = 0.5f;

    //misc
    private static int enemiesKilled;
    private static int score;
    private static bool gameOver;


    public static float NorthEdgeLimit { get => northEdgeLimit; }
    public static float EastEdgeLimit { get => eastEdgeLimit; }
    public static int FirstWaveAmount { get => firstWaveAmount; }
    public static int WaveIncrement { get => waveIncrement; }
    public static int EnemiesKilled { get => enemiesKilled; set => enemiesKilled = value; }
    public static int EnemyHealth { get => enemyHealth;}
    public static float EnemyDetectionRange { get => enemyDetectionRange;}
    public static float EnemyAttackRange { get => enemyAttackRange;}
    public static int PlayerHealth { get => playerHealth;}
    public static float EnemyDetectedSpeed { get => enemyDetectedSpeed;  }
    public static float EnemyRandomSpeed { get => enemyRandomSpeed; }
    public static int Score { get => score; set => score = value; }
    public static float Cooldown { get => cooldown;}
    public static bool GameOver { get => gameOver; set => gameOver = value; }
}
