namespace Assets.Minamo.Editor {
    public class Version {
        public readonly static int Major = 1;
        public readonly static int Minor = 0;
        public readonly static int Patch = 0;

        public override string ToString() {
            var tokens = new string[]
            {
                Major.ToString(),
                Minor.ToString(),
                Patch.ToString(),
            };
            return string.Join(".", tokens);
        }
    }
}
