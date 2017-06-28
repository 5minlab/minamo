using Assets.Minamo.Editor;
using UnityEngine;

namespace Assets.Editor {
    public class BuildScript {
        static void CustomBuild() {
            Debug.Log("Input your pre-build action");
            Build.Execute();
            Debug.Log("Input yout post-build action");
        }
    }
}