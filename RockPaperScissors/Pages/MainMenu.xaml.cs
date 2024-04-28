namespace RockPaperScissors.Pages;

public partial class MainMenu : ContentPage
{
	public MainMenu()
	{
		InitializeComponent();
	}
    async void OnNewGameClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Game");
    }

    async void OnStatisticsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Statistics");
    }
}