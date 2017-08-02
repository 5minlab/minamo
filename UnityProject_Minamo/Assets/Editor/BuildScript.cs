using Assets.Minamo.Editor;
using UnityEngine;

namespace Assets.Editor {
    class BuildScript {
        static void CustomBuild() {
            Debug.Log("Input your pre-build action");
            EntryPoint.Build();
            Debug.Log("Input yout post-build action");
        }
    }
}