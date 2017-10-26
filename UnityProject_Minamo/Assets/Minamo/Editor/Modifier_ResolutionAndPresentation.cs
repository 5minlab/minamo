using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class Modifier_ResolutionAndPresentation : IModifier {
        // resolution
        bool runInBackground;

        // standalone player

        // android player

        // uwp player

        internal Modifier_ResolutionAndPresentation() {
        }

        Modifier_ResolutionAndPresentation(Dictionary<string, object> map) {
            var dict = new AnyDictionary(map);
        }

        public void Apply() {
            Application.runInBackground = runInBackground;
        }

        internal static Modifier_ResolutionAndPresentation Current() {
            return new Modifier_ResolutionAndPresentation()
            {
                runInBackground = Application.runInBackground,
            };
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("RunInBackground={0}, ", runInBackground);
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            runInBackground = dict.GetValue<bool>("runInBackground", false);
        }
    }
}
