using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;

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
            MessagingCenter.Subscribe<String>(this, "SendCredentials", (sender) => {
                MessagingCenter.Send(currentLoggedInUser, "GetCredentials");
            });
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseApiKey));
        }

        public FirebaseAuthLink getCurrentUser()
        {
            return currentLoggedInUser;
        }

        //Create a new user
        public async void CreateNewUser(String email, String password)
        {
            currentLoggedInUser = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, email, true);
        }

        //Login user with email and password to firebase
        public bool LoginUser(String email, String password)
        {
            currentLoggedInUser = authProvider.SignInWithEmailAndPasswordAsync(email, password).Result;
            return currentLoggedInUser != null;
        }


        //Reset password
        public async void ResetPassword(String email)
        {
            await authProvider.SendPasswordResetEmailAsync(email);
        }

        //Delete account
        public async void DeleteAccount(String email, String password)
        {
            currentLoggedInUser = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
        }
    }
}
