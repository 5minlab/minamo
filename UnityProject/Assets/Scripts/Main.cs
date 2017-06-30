using UnityEngine;

class Main : MonoBehaviour {
    void Start () {
#if PLATFORM_WIN32_STEAMVR
        Debug.LogError("current platform is PLATFORM_WIN32_STEAMVR");
#endif

#if PLATFORM_WIN32_OCULUS
        Debug.LogError("current platform is PLATFORM_WIN32_OCULUS");
#endif


#if HELLO_WORLD
        Debug.LogError("hello world!");
#endif

        Debug.LogError("started");
    }
}
