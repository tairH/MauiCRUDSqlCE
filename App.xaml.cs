namespace MauiCRUDSqlCE;

public partial class App : Application
{
	public App(MainPage mp)
	{
		InitializeComponent();

		MainPage = mp;//new AppShell();
	}
}
