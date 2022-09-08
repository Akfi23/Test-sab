using Kuhpik;
using Supyrb;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppFocus : DontDestroySingleton<AppFocus>
{
    bool isPaused = false;
    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;

        if (isPaused == true)
        {
            Signals.Get<OnAppQuitSignal>().Dispatch();
        }
        else
        {

        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
        Signals.Get<OnAppQuitSignal>().Dispatch();
    }
}
