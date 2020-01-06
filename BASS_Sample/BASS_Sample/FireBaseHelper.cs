using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASS_Sample
{
   public class FireBaseHelper
    {
        FirebaseClient firebase;
        public FireBaseHelper()
        {
          firebase = new FirebaseClient("https://stunning-grin-700.firebaseio.com/");
        }

        public async Task<List<User>> GetAllPersons()
        {

            return (await firebase
              .Child("Persons")
              .OnceAsync<User>()).Select(item => new User
              {
                  Name = item.Object.Name,
                  OrgName = item.Object.OrgName,
                  Gender=item.Object.Gender
              }).ToList();
        }
        public async Task AddPerson(string name, string orgName,string gender)
        {
            
            await firebase
              .Child("Persons") 
              .PostAsync(new User() { Name=name,OrgName=orgName,Gender= gender });
        }
        public async Task<User> GetPerson(string name)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<User>();
            return allPersons.Where(a => a.Name == name).FirstOrDefault();
        }

        public async Task UpdatePerson(  string name, string orgName,string gender)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<User>()).Where(a => a.Object.Name == name).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new User() { OrgName = orgName, Name = name ,Gender= gender });
        }

        public async Task DeletePerson(string name)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<User>()).Where(a => a.Object.Name == name).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }

    }
}
