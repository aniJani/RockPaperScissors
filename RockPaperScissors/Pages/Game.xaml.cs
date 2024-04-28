using System;
using Microsoft.Maui.Controls;

namespace RockPaperScissors.Pages;

public partial class Game : ContentPage
{
    private static readonly Random random = new Random();  // Random is now static to avoid re-seeding issues.

    public Game()
    {
        InitializeComponent();
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            string playerChoice = button.ClassId;
            string computerChoice = GetComputerChoice();
            string result = DetermineWinner(playerChoice, computerChoice);
            DisplayResult(result, playerChoice, computerChoice);
            DisableChoiceButtons(); // Call this method to disable the buttons
        }
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
            "Rock" => "🪨",  // Emoji for Rock
            "Paper" => "📄", // Emoji for Paper
            "Scissors" => "✂️", // Emoji for Scissors
            _ => string.Empty
        };
    }


    private void OnNextRoundClicked(object sender, EventArgs e)
    {
        // Re-enable the buttons for the next round
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
