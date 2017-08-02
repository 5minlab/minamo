using System.Text;
using UnityEngine;
using UnityEngine.UI;

class Main : MonoBehaviour {
    Text text;
    private void Awake() {
        text = GetComponentInChildren<Text>();
    }

    void Start () {
        var sb = new StringBuilder();


#if PLATFORM_WIN32_STEAMVR
        sb.AppendLine("current platform is PLATFORM_WIN32_STEAMVR");
#endif

#if PLATFORM_WIN32_OCULUS
        sb.AppendLine("current platform is PLATFORM_WIN32_OCULUS");
#endif


#if HELLO_WORLD
        sb.AppendLine("hello world!");
#endif

        if(Debug.isDebugBuild) {
            sb.AppendLine("this is debug build");
        } else {
            sb.AppendLine("this is release build");
        }

        text.text = sb.ToString();
    }
}
