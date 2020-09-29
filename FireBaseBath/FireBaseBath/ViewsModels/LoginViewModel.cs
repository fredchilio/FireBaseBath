using FireBaseBath.Services;
using FireBaseBath.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FireBaseBath.ViewsModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public String Username
        {
            set
            {
                this._Username = value;
                OnpropertyChanged();
            }
            get
            {
                return this._Username;
            }
        }

        private string _Password;
        public String Password
        {
            set
            {
                this._Password = value;
                OnpropertyChanged();
            }
            get
            {
                return this._Password;
            }
        }
        private bool _Result;
        public bool Result
        {
            set
            {
                this._IsBusy = value;
                OnpropertyChanged();
            }
            get 
            {
               return this._IsBusy;
            }

        }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                this._Result = value;
                OnpropertyChanged();
            }
            get
            {
                return this._Result;
            }

        }
        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }


        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.RegisterUser(Username, Password);
                if (Result)
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuario Registrado", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao Registrar Usuario", "OK");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.LoginUser(Username, Password);
                if(Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushAsync(new ContactView());

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Usuario/Senha inválido(s)", "OK");
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
