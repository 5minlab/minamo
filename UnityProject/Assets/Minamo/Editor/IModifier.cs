namespace Assets.Minamo.Editor {
    public interface IModifier {
        void Apply();
        string GetConfigText();
    }
}
