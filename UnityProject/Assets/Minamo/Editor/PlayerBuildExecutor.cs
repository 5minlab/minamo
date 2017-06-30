using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class PlayerBuildExecutor {
        static BuildOptions GetOptions(Dictionary<string, object> map) {
            var opts = BuildOptions.None;
            foreach (var kv in map) {
                if (kv.Value.GetType() != typeof(bool)) {
                    continue;
                }
                var mask = Helper.ToBuildOptions(kv.Key);
                opts = opts | mask;
            }
            return opts;
        }

        internal readonly BuildTarget Target = BuildTarget.NoTarget;
        internal readonly BuildTargetGroup TargetGroup;
        readonly BuildOptions Options = BuildOptions.None;
        readonly string[] scenes = new string[] { };

        internal PlayerBuildExecutor(AnyDictionary dict) {
            Target = Helper.ToBuildTarget(dict.GetValue<string>("target"));
            TargetGroup = Helper.ToBuildTargetGroup(dict.GetValue<string>("targetGroup"));

            var optionmap = dict.GetDict("options");
            Options = GetOptions(optionmap);

            this.scenes = GetScenes();
        }

        internal string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("buildTarget={0}, ", Target);
            sb.AppendFormat("buildTargetGroup={0}, ", TargetGroup);
            sb.AppendFormat("options={0}, ", Options);
            sb.AppendFormat("scenes={0}", string.Join(",", scenes));
            return sb.ToString();
        }

        static string[] GetScenes() {
            List<string> scenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
                if (scene.enabled) {
                    scenes.Add(scene.path);
                }
            }
            return scenes.ToArray();
        }

        internal void Build(string locationPathName) {
            BuildPipeline.BuildPlayer(scenes, locationPathName, Target, Options);
        }
    }
}
