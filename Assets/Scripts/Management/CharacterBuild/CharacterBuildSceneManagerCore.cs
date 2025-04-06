namespace Management.CharacterBuild
{
    public class CharacterBuildSceneManagerCore
    {
        public CharacterBuildSceneManager Parent { get; set; }
        
        public CharacterBuildSceneManagerCore(CharacterBuildSceneManager parent)
        {
            Parent = parent;
        }

        public void OnAwake() { }
    }
}