using Firebase.Auth;

namespace SEGP7.Firebase
{
    public class FirebaseAuthenticationController
    {
        //Connect to firebase authentication
        FirebaseAuthProvider authProvider;
        FirebaseAuthLink currentLoggedInUser;
        String FirebaseApiKey = "AIzaSyA-NBm1ekoHirJT9Y_bg2mZux9zN9jopBw";

        public FirebaseAuthenticationController()
        {
            RegisterAndSendMessages();
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseApiKey));
        }

        public FirebaseAuthLink getCurrentUser()
        {
            return currentLoggedInUser;
        }

        //Create a new user
        public String CreateNewUser(String email, String password)
        {
            try
            {
                currentLoggedInUser = authProvider.CreateUserWithEmailAndPasswordAsync(email, password, email, true).Result;
                if (currentLoggedInUser != null)
                {
                    MessagingCenter.Send(email, "NewUserBitIO");
                    return "";
                }
            } catch(Exception e) {
                return e.Message;
            }
            return "Reason: Unknown";
        }

        //Login user with email and password to firebase
        public bool LoginUser(String email, String password)
        {
            currentLoggedInUser = authProvider.SignInWithEmailAndPasswordAsync(email, password).Result;
            return currentLoggedInUser != null;
        }

        //Reset password
        public void ResetPassword(String email)
        {
             authProvider.SendPasswordResetEmailAsync(email);
        }

        //Delete account
        public bool DeleteAccount()
        {
            String currentEmail = currentLoggedInUser.User.Email;
            bool accountDeleted = false;
            authProvider.DeleteUserAsync(currentLoggedInUser.FirebaseToken).ContinueWith(task =>
            {
                accountDeleted = true;
            });
            if (accountDeleted)
            {
                MessagingCenter.Send(currentEmail, "DeleteUser");
            }
            return accountDeleted;
        }

        void RegisterAndSendMessages()
        {
            MessagingCenter.Subscribe<String>(this, "SendCredentials", (sender) => {
                MessagingCenter.Send(currentLoggedInUser, "GetCredentials");
            });
            MessagingCenter.Subscribe<String>(this, "ResetPassword", (email) => {
                ResetPassword(email);
            });
        }

        void ChangePassword(String newPassword)
        {
            currentLoggedInUser = authProvider.ChangeUserPassword(currentLoggedInUser.FirebaseToken, newPassword).Result;
        }
    }
}