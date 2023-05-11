using BTLAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;

namespace BTLAPI.APIController
{
    public class UsersController : ApiController
    {
        DBContextcs db = new DBContextcs();

        public List<User> GetUsers()
        {
            List<User> users = db.Users.ToList();
            return users;
        }
        [HttpGet]
        [Route("api/users/getid")]
        public User getUserById(int id)
        {
            User user = db.Users.Where(row => row.UserID == id).FirstOrDefault();
            return user;
        }
        public User Login(string email, string password)
        {
            var hashedPassword = GetMD5(password);
            User user = db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(password)).FirstOrDefault();
            return user;
        }

        public User GetUserByEmail(string Email)
        {
            User user = db.Users.Where(row => row.Email == Email).FirstOrDefault();
            return user;
        }

        [HttpPost]
        public void CreateUser(User user)
        {
            user.Password = GetMD5(user.Password);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Users.Add(user);
            db.SaveChanges();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [HttpPut]
        public void EditUser(User u)
        {
            User user = db.Users.Where(row => row.UserID == u.UserID).FirstOrDefault();
            user.FullName = u.FullName;
            user.Email = u.Email;
            user.Password = GetMD5(u.Password);
            user.isAdmin = u.isAdmin;
            db.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            User user = db.Users.Where(row => row.UserID == id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
