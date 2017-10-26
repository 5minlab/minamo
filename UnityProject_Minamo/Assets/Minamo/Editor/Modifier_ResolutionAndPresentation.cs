using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class Modifier_ResolutionAndPresentation : IModifier {
        // resolution
        bool runInBackground;
        bool runInBackground_use;

        // standalone player

        // android player

        // uwp player

        internal Modifier_ResolutionAndPresentation() {
        }

        Modifier_ResolutionAndPresentation(Dictionary<string, object> map) {
            var dict = new AnyDictionary(map);
        }

        public void Apply() {
            if (runInBackground_use) {
                Application.runInBackground = runInBackground;
            }
        }

        internal static Modifier_ResolutionAndPresentation Current() {
            return new Modifier_ResolutionAndPresentation()
            {
                runInBackground = Application.runInBackground,
            };
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            if (runInBackground_use) {
                sb.AppendFormat("RunInBackground={0}, ", runInBackground);
            }
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            runInBackground_use = dict.TryGetValue<bool>("runInBackground", out runInBackground);
        }
    }
}
