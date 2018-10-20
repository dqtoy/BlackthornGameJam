using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugUtils {

    public delegate void BugKilled();
    public static event BugKilled OnBugKilled;


    public static void KillBugOccur()
    {
        OnBugKilled();
    }
}
