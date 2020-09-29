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

        private void BtnShow_Clicked(object sender, EventArgs e)
        {

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
        private void BtnReload_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {

        }
    }
}