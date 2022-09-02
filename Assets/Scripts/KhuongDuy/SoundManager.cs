using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource
        musicMatch,
        buttonClick,
        refereeStartGame,
        refereeEndGame,
        jump,
        ballHit,
        ballKick,
        goal1,
        goal2,
        matchWon,
        upgradeButton,
        coinEffect,
        matchLost;
    public AudioSource musicBG;
    public AudioClip music1, music2;
    internal object referee;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        
    }
}
