using Sat.Recruitment.Data.Repository.Interfaces;
using Sat.Recruitment.Model.Users;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Data.Repository.Operations
{
    public class UserRepository : IUserRepository
    {
        ICollection<UsersModel> users = new List<UsersModel>();

        public async Task<bool> UserExistAsync(UsersModel model)
        {
            // Open the text file using a stream reader.
            foreach (string line in File.ReadLines(Directory.GetCurrentDirectory() + "/Files/Users.txt"))
            {
                var user = line.Split(",");

                users.Add(new UsersModel
                {
                    Name = user[0],
                    Email = user[1],
                    Phone = user[2],
                    Address = user[3],
                    UserType = user[4],
                    Money = decimal.Parse(user[5])
                });
            }

            if(users.AsQueryable().Where(x => x.Name.Equals(model.Name) || x.Phone.Equals(model.Phone) || x.Email.Equals(model.Email)).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveAsync(UsersModel model)
        {
            // Append text to an existing file named "Users.txt".
            using(StreamWriter usersFile = new StreamWriter(Directory.GetCurrentDirectory() + "/Files/Users.txt", true))
            {
                usersFile.WriteLine(string.Format("{0},{1},{2},{3},{4},{5}", model.Name.Trim(), model.Email.Trim(), model.Phone.Trim(), model.Address.Trim(), model.UserType.Trim(), model.Money));
            }
        }
    }
}
