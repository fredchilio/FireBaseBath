using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using FireBaseBath.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FireBaseBath.Services
{
    public class ContatoService
    {
        FirebaseClient firebase = new FirebaseClient("https://liffeybath.firebaseio.com/");
    
    public async Task AddContato(int contatoId, string name, string email)
        {
            await firebase.Child("Contatos")
                .PostAsync(new Contato() { ContatoId = contatoId, Name = name, Email = email });
        }
        public async Task<List<Contato>> GetContatos()
        {
            return (await firebase
                .Child("Contatos")
                .OnceAsync<Contato>()).Select(item => new Contato
                {
                    Name = item.Object.Name,
                    Email = item.Object.Email,
                    ContatoId = item.Object.ContatoId
                }).ToList();
        }
    
         public async Task<Contato> GetContato(int contatoId)
        {
            try
            {
                var contato = (await firebase
                    .Child("Contatos")
                    .OnceAsync<Contato>())
                    .Where(a => a.Object.ContatoId == contatoId).FirstOrDefault();

                return await firebase.Child("Contatos")
                    .Child(contato.Key).OnceSingleAsync<Contato>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateContatos(int contatoId, string name, string email)
        {
            try
            {
                var toUpdateContato = (await firebase
                    .Child("Contatos")
                    .OnceAsync<Contato>())
                    .Where(a => a.Object.ContatoId == contatoId).FirstOrDefault();
                await firebase
                    .Child("Contatos")
                    .Child(toUpdateContato.Key)
                    .PutAsync(new Contato()
                    {
                        ContatoId = contatoId,
                        Name = name,
                        Email = email
                    });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteContato(int contatoId)
        {
            try
            {
                var toDeleteContato = (await firebase
                    .Child("Contatos")
                    .OnceAsync<Contato>())
                    .Where(a => a.Object.ContatoId == contatoId).FirstOrDefault();

                await firebase.Child("Contatos")
                    .Child(toDeleteContato.Key)
                    .DeleteAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
