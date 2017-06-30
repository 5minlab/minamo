namespace Assets.Minamo.Editor {
    public interface IModifier {
        void Reload(AnyDictionary dict);
        void Apply();
        string GetConfigText();
    }
}
