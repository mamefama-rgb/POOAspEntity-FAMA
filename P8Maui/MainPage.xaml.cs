namespace P8Maui;

public partial class MainPage : ContentPage
{
    string? translatedNumber;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Appeler " + translatedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Appeler";
        }
    }

    private async void OnCall(object sender, EventArgs e)
    {
        if (await DisplayAlert("Composer un numéro",
                "Voulez-vous appeler " + translatedNumber + " ?", "Oui", "Non"))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                {
                    PhoneDialer.Default.Open(translatedNumber!);
                }
                else
                {
                    await DisplayAlert("Non pris en charge",
                        "Les appels ne sont pas disponibles sur cet appareil.", "OK");
                }
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Appel impossible", "Le numéro n'est pas valide.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Appel impossible", "L'appel a échoué.", "OK");
            }
        }
    }
}