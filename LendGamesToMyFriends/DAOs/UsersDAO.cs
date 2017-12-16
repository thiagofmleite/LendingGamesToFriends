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

        public UserReference Authenticate(string email, string password)
        {
            var user = GetUserByEmail(email);
            var md5pass = ConvertToMD5(password);
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
            user.Password = ConvertToMD5(user.Password);
            user = context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public bool UserExists(string email)
        {
            var user = GetUserByEmail(email);
            return (user != null);
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
    }
}