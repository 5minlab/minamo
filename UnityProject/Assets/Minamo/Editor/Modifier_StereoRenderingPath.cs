using System.Text;
using UnityEditor;

namespace Assets.Minamo.Editor {
    class Modifier_StereoRenderingPath : IModifier {
        StereoRenderingPath stereoRenderingPath;

        public void Apply() {
            PlayerSettings.stereoRenderingPath = stereoRenderingPath;
        }

        public string GetConfigText() {
            var sb = new StringBuilder();
            sb.AppendFormat("stereoRenderingPath={0}", stereoRenderingPath);
            return sb.ToString();
        }

        public void Reload(AnyDictionary dict) {
            var table = StringEnumConverter.Get<StereoRenderingPath>();
            stereoRenderingPath = table[dict.GetValue<string>("stereoRenderingPath")];
        }

        public static Modifier_StereoRenderingPath Current() {
            return new Modifier_StereoRenderingPath()
            {
                stereoRenderingPath = PlayerSettings.stereoRenderingPath,
            };
        }
    }
}
