namespace Assets.Minamo.Editor {
    interface IModifier {
        void Reload(AnyDictionary dict);
        void Apply();
        string GetConfigText();
    }
}
