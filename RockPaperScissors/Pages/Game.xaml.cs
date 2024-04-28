using System;
using Microsoft.Maui.Controls;
using RockPaperScissors.Database;
using RockPaperScissors.Model;

namespace RockPaperScissors.Pages;
public partial class Game : ContentPage
{
    private static readonly Random random = new Random();
    private readonly LocalDB _database;
    private Model.Statistics _currentStats;

    public Game()
    {
        InitializeComponent();
        _database = new LocalDB();
        LoadStatistics();
    }

    private async void LoadStatistics()
    {
        _currentStats = await _database.GetStatisticsAsync();
        if (_currentStats == null)
        {
            _currentStats = new Model.Statistics();
        }
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            string playerChoice = button.ClassId;
            string computerChoice = GetComputerChoice();
            string result = DetermineWinner(playerChoice, computerChoice);
            DisplayResult(result, playerChoice, computerChoice);
            UpdateStatistics(playerChoice, result);
            DisableChoiceButtons();
        }
    }

    private void UpdateStatistics(string playerChoice, string result)
    {
        _currentStats.TotalGamesPlayed++;
        switch (playerChoice)
        {
            case "Rock":
                _currentStats.PlayerChoseRock++;
                break;
            case "Paper":
                _currentStats.PlayerChosePaper++;
                break;
            case "Scissors":
                _currentStats.PlayerChoseScissors++;
                break;
        }
        if (result == "You win!")
            _currentStats.PlayerWins++;
        else if (result == "You lose!")
            _currentStats.AIWins++;

        Task.Run(async () => await _database.UpdateStatisticsAsync(_currentStats));
    }

    private void DisableChoiceButtons()
    {
        ScissorsButton.IsEnabled = false;
        RockButton.IsEnabled = false;
        PaperButton.IsEnabled = false;
    }

    private string GetComputerChoice()
    {
        string[] choices = { "Rock", "Paper", "Scissors" };
        int index = random.Next(choices.Length);
        return choices[index];
    }

    private string DetermineWinner(string player, string computer)
    {
        if (player == computer) return "It's a draw!";
        if ((player == "Rock" && computer == "Scissors") ||
            (player == "Paper" && computer == "Rock") ||
            (player == "Scissors" && computer == "Paper"))
        {
            return "You win!";
        }
        return "You lose!";
    }

    private void DisplayResult(string result, string playerChoice, string computerChoice)
    {
        PlayerChoiceLabel.Text = GetEmojiForChoice(playerChoice);
        ComputerChoiceLabel.Text = GetEmojiForChoice(computerChoice);
        ResultLabel.Text = result;
    }

    private string GetEmojiForChoice(string choice)
    {
        return choice switch
        {
            "Rock" => "🪨",
            "Paper" => "📄",
            "Scissors" => "✂️",
            _ => string.Empty
        };
    }

    private void OnNextRoundClicked(object sender, EventArgs e)
    {
        ScissorsButton.IsEnabled = true;
        RockButton.IsEnabled = true;
        PaperButton.IsEnabled = true;
        PlayerChoiceLabel.Text = string.Empty;
        ComputerChoiceLabel.Text = string.Empty;
        ResultLabel.Text = string.Empty;
    }

    async void OnExitClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainMenu");
    }
}