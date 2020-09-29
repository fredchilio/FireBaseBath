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
    
            
    }
}
