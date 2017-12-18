using LendGamesToMyFriends.Context;
using LendGamesToMyFriends.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LendGamesToMyFriends.DAOs
{
    public class UsersDAO : IUsersDAO
    {
        private LendGamesContext context;

        public UsersDAO()
        {
            context = new LendGamesContext();
        }

        public UserReference Authenticate(Login login)
        {
            var user = GetUserByEmail(login.Email);
            var md5pass = ConvertToMD5(login.Password);
            if(user.Password != md5pass)
            {
                throw new Exception("Senha inválida");
            }
            else
            {
                return new UserReference
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public User Register(User user)
        {
            user.Id = Guid.NewGuid();
            user.Password = ConvertToMD5(user.Password);
            user = context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public bool UserExists(string email)
        {
            return context.Users.Any(u => u.Email.Equals(email));
        }

        private string ConvertToMD5(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hash = md5.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}