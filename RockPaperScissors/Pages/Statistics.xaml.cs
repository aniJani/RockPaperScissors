using RockPaperScissors.Database;

namespace RockPaperScissors.Pages;

public partial class Statistics : ContentPage
{
    private readonly LocalDB _database;
    private Model.Statistics _stats;

    public Statistics()
    {
        InitializeComponent();
        _database = new LocalDB();
        LoadStatisticsAsync();
    }

    private async void LoadStatisticsAsync()
    {
        _stats = await _database.GetStatisticsAsync() ?? new Model.Statistics();
        UpdateUI();
    }

    private void UpdateUI()
    {
        TotalGamesLabel.Text = $"Total Games Played: {_stats.TotalGamesPlayed}";
        PlayerWinsLabel.Text = $"Player Wins: {_stats.PlayerWins}";
        AIWinsLabel.Text = $"AI Wins: {_stats.AIWins}";
        PlayerChoiceLabel.Text = $"Player Choices: Rock ({_stats.PlayerChoseRock}), Paper ({_stats.PlayerChosePaper}), Scissors ({_stats.PlayerChoseScissors})";
        DrawsLabel.Text = $"Draws: {_stats.TotalGamesPlayed - (_stats.PlayerWins + _stats.AIWins)}";
    }

    private async void GoToMainMenu(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainMenu");
    }
}