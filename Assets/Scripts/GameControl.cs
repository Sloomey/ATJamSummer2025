using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int gameWeek = 1;

    public static GameControl Instance { get; private set; } // Global access point.

    private void Awake()
    {
        if (Instance != null && Instance != this) // Check if another instance already exists.
        {
            Destroy(gameObject); // Destroy the new instance if another exists.
        }
        else
        {
            Instance = this; // Set this instance as the singleton instance.
            DontDestroyOnLoad(gameObject); // Keep the singleton alive across scenes if needed.
        }
    }

    public void NewWeek()
    {
        gameWeek += 1;
    }
}
