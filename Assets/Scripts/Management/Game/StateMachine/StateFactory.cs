namespace Management.Game.StateMachine
{
    public class StateFactory : IStateFactory
    {
        // public GameManagerCore.CreditsState CreditsState { get; }
        // public GameManagerCore.HelpState HelpState { get; }
        // public GameManagerCore.InitState InitState { get; }
        // public GameManagerCore.PauseState PauseState { get; }
        // public GameManagerCore.PlayState PlayState { get; }
        // public GameManagerCore.PreRollState PreRollState { get; }
        // public GameManagerCore.QuitState QuitState { get; }
        public GameManagerCore.MainMenuState MainMenuState { get; }
        public GameManagerCore.CharacterBuildState CharacterBuildState { get; }

        public StateFactory(GameManagerCore gameManagerCore)
        {
            // this.CreditsState = new GameManagerCore.CreditsState(gameManagerCore);
            // this.HelpState = new GameManagerCore.HelpState(gameManagerCore);
            // this.InitState = new GameManagerCore.InitState(gameManagerCore);
            // this.PauseState = new GameManagerCore.PauseState(gameManagerCore);
            // this.PlayState = new GameManagerCore.PlayState(gameManagerCore);
            // this.PreRollState = new GameManagerCore.PreRollState(gameManagerCore);
            // this.QuitState = new GameManagerCore.QuitState(gameManagerCore);
            this.MainMenuState = new GameManagerCore.MainMenuState(gameManagerCore);
            this.CharacterBuildState = new GameManagerCore.CharacterBuildState(gameManagerCore);

        }
    }
}
