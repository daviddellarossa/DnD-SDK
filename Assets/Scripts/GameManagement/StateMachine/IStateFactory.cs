namespace GameManagement.StateMachine
{
    internal interface IStateFactory
    {
        // GameManagerCore.CreditsState CreditsState { get; }
        // GameManagerCore.HelpState HelpState { get; }
        // GameManagerCore.InitState InitState { get; }
        // GameManagerCore.PauseState PauseState { get; }
        // GameManagerCore.PlayState PlayState { get; }
        // GameManagerCore.PreRollState PreRollState { get; }
        // GameManagerCore.QuitState QuitState { get; }
        GameManagerCore.SampleSceneState SampleSceneState { get; }
    }
}