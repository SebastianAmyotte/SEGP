namespace SEGP7.Authentication;
using SEGP7.Firebase;
using SEGP7.Tools;

public partial class RegisterPage : ContentPage
{
    FirebaseAuthenticationController authController;

    public RegisterPage(FirebaseAuthenticationController authController)
    {
        this.authController = authController;
        InitializeComponent();
    }

    private void RegisterUserButtonPushed(object sender, EventArgs e)
    {
        //CreateNewUser
        String registrationResult = authController.CreateNewUser(emailEntry.Text.ToLower(), passwordEntry.Text);
        if (registrationResult == "") {
            new DiskIO("LastUserEmail.txt").WriteToFile(emailEntry.Text.ToLower());
            DisplayAlert("Success", "New account created succesfully", "OK");
        } else {
            registrationResult = registrationResult.Split("Reason:")[1];
            registrationResult = registrationResult.Remove(registrationResult.Length - 1);
            DisplayAlert("Error", registrationResult, "OK");
        }
}

    private void CancelRegisterButtonPushed(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}