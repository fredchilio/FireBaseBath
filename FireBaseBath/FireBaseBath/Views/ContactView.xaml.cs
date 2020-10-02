using FireBaseBath.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FireBaseBath.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactView : ContentPage
    {
        ContatoService contatoService = new ContatoService();
        public ContactView()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            await ShowContact();
        }

        private async void BtnShow_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Error", "Invalid Id", "Ok");
            }
            else
            {
                try
                {
                    var contato = await contatoService.GetContato(Convert.ToInt32(txtId));
                    if (contato !=null)
                    {
                        txtId.Text = contato.ContatoId.ToString();
                        txtName.Text = contato.Name;
                        txtName.Text = contato.Email;

                    }
                    else
                    {
                        await DisplayAlert("Error", "Id wrong", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "User did not find" + ex.Message, "Ok");  
                }
            }
        }

        private async void BtnInclude_Clicked(object sender, EventArgs e)
        {
            await contatoService.AddContato(Convert.ToInt32(txtId.Text), txtName.Text, txtEmail.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            await DisplayAlert("Sucess", "Contato included", "Ok");
            await ShowContact();
        }

        private async Task ShowContact()
        {
            var contatos = await contatoService.GetContatos();
            ListContact.ItemsSource = contatos;
        }
        private async void BtnUpDate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Error", "Id wrong", "Ok");
            }
            else
            {
                try
                {
                    await contatoService
                        .UpdateContatos(Convert.ToInt32(txtId.Text), txtName.Text, txtEmail.Text);
                    txtId.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtEmail.Text = string.Empty;

                    await DisplayAlert("Sucesso", "UpDate", "Ok");

                    await ShowContact();
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", "User did not find" + ex.Message, "Ok");
                }
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Error", "Id wrong", "Ok");
            }
            else
            {
                try
                {
                    await contatoService
                        .DeleteContato(Convert.ToInt32(txtId.Text));
                    txtId.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtEmail.Text = string.Empty;

                    await DisplayAlert("Sucesso", "Deleted", "Ok");

                    await ShowContact();
                }
                catch (Exception ex)
                {

                    await DisplayAlert("Error", "User did not Delete" + ex.Message, "Ok");
                }
            }

        }
    }
}