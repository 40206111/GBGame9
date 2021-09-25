#if DEVELOPMENT_BUILD || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public static bool ForceLoadSceneFromBoot;
    public static string SceneToLoadFromBoot;


}
#endif