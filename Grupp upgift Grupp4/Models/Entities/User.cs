using System.Text.Json.Serialization;

namespace Grupp_upgift_Grupp4.Models.Entities
{
    public class User
    {
        [JsonIgnore]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(int userID, string userName, string password, string firstName, string lastName)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
