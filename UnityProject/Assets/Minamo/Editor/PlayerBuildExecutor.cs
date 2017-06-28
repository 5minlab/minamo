using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Minamo.Editor {
    class PlayerBuildExecutor {
        public const string KeyTarget = "target";
        public const string KeyTargetGroup = "targetGroup";
        public const string KeyOptions = "options";


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

        public readonly BuildTarget Target = BuildTarget.NoTarget;
        public readonly BuildTargetGroup TargetGroup;
        public readonly BuildOptions Options = BuildOptions.None;
        readonly string[] scenes = new string[] { };

        public PlayerBuildExecutor(Dictionary<string, object> map) {
            if(map.ContainsKey(KeyTarget)) {
                var s = map[KeyTarget] as string;
                if (s != null) {
                    Target = Helper.ToBuildTarget(s);
                }
            }
            if(Target == BuildTarget.NoTarget) {
                Debug.Log("undefined target?");
            }


            if (map.ContainsKey(KeyTargetGroup)) {
                var s = map[KeyTargetGroup] as string;
                if (s != null) {
                    TargetGroup = Helper.ToBuildTargetGroup(s);
                }
            }


            if (map.ContainsKey(KeyOptions)) {
                var omap = map[KeyOptions] as Dictionary<string, object>;
                if(omap != null) {
                    Options = GetOptions(omap);
                }
            }

            this.scenes = GetScenes();
        }

        public string GetConfigText() {
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

        public void Build(string locationPathName) {
            BuildPipeline.BuildPlayer(scenes, locationPathName, Target, Options);
        }
    }
}
