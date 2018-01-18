using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class Modifier_ResolutionAndPresentation : IModifier {
        // resolution
        AssignableType<bool> runInBackground;

        // standalone player

        // android player

        // uwp player

        internal Modifier_ResolutionAndPresentation() {
        }

        Modifier_ResolutionAndPresentation(Dictionary<string, object> map) {
            var dict = new AnyDictionary(map);
        }

        public void Apply() {
            if (runInBackground.Flag) {
                Application.runInBackground = runInBackground;
            }
        }

        internal static Modifier_ResolutionAndPresentation Current() {
            return new Modifier_ResolutionAndPresentation()
            {
                runInBackground = AssignableType<bool>.Create(Application.runInBackground),
            };
        }

        public string GetConfigText() {
            var cb = new ConfigTextBuilder();
            cb.Append("runInBackground", runInBackground);
            return cb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            runInBackground = AssignableType<bool>.FromDict(dict, "runInBackground");
        }
    }
}
