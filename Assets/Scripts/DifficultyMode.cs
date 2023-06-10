using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameModes
{
    // Define values for Game Mode enum: None, MultiPlayer, SinglePlayerEasy, SinglePlayerMedium, SinglePlayerHard and PC Vs PC all modes
    None,
    MultiPlayer,
    SinglePlayerEasy,
    SinglePlayerMedium,
    SinglePlayerHard,
    EasyPCEasyPC,
    EasyPCMediumPC,
    EasyPCHardPC,
    MediumPCEasyPC,
    MediumPCMediumPC,
    MediumPCHardPC,
    HardPCEasyPC,
    HardPCMediumPC,
    HardPCHardPC
}

public class DifficultyMode : MonoBehaviour
{
    public static GameModes gameMode = GameModes.None;

    [SerializeField]
    public Slider sliderSingle;

    [SerializeField]
    public Slider sliderPC1;

    [SerializeField]
    public Slider sliderPC2;


    public void ResetSliderPosition()
    {
        sliderSingle.value = 0;
        sliderPC1.value = 0;
        sliderPC2.value = 0;
    }

    public void OnMultiPlayerClicked()
    {
        gameMode = GameModes.MultiPlayer;
    }

    public void OnGoSinglePlayerClicked()
    {
        switch (sliderSingle.value)
        {
            case 0:
                gameMode = GameModes.SinglePlayerEasy;
                break;
            case 1:
                gameMode = GameModes.SinglePlayerMedium;
                break;
            case 2:
                gameMode = GameModes.SinglePlayerHard;
                break;
            default: break;
        }
    }

    public void OnGoPCvsPCClicked()
    {
        switch (sliderPC1.value)
        {
            case 0:
                switch (sliderPC2.value)
                {
                    case 0:
                        gameMode = GameModes.EasyPCEasyPC;
                        break;
                    case 1:
                        gameMode = GameModes.EasyPCMediumPC;
                        break;
                    case 2:
                        gameMode = GameModes.EasyPCHardPC;
                        break;
                    default: break;
                }
                break;
            case 1:
                switch (sliderPC2.value)
                {
                    case 0:
                        gameMode = GameModes.MediumPCEasyPC;
                        break;
                    case 1:
                        gameMode = GameModes.MediumPCMediumPC;
                        break;
                    case 2:
                        gameMode = GameModes.MediumPCHardPC;
                        break;
                    default: break;
                }
                break;
            case 2:
                switch (sliderPC2.value)
                {
                    case 0:
                        gameMode = GameModes.HardPCEasyPC;
                        break;
                    case 1:
                        gameMode = GameModes.HardPCMediumPC;
                        break;
                    case 2:
                        gameMode = GameModes.HardPCHardPC;
                        break;
                    default: break;
                }
                break;
            default: break;
        }

    }

}
