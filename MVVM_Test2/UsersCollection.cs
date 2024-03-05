using System.Collections.ObjectModel;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MVVM_Test2;
public class UsersCollection : List<User>
{
    public const string UsersFile = "users.data";
    public void LoadUsers()
    {
        Clear();
        try
        {
            using FileStream fs = new(UsersFile, FileMode.Open);
            JsonSerializer.Deserialize<List<User>>(fs)?.ForEach(Add);
        }
        catch
        {
        }
    }

    public void AddUser(User user)
    {
        var foundUser = this.FirstOrDefault(u => u.Nick.Equals(user.Nick));
        if (foundUser != null)
            foundUser.UpdateInfoFrom(user);
        else Add(user);
        SaveAllUsers();
    }

    private void SaveAllUsers()
    {
        try
        {
            using FileStream fs = new(UsersCollection.UsersFile, FileMode.Create);
            var opt = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            JsonSerializer.Serialize(fs, this, opt);
        }
        catch
        {
        }
    }
    public void RemoveUser(User user)
    {
        var u = this.FirstOrDefault(u => u.Nick.Equals(user.Nick));
        if (u != null)
        {
            Remove(u);
            SaveAllUsers();
        }
    }

    public User? GetUserByNick(string nick) => this.FirstOrDefault(u => u.Nick.Equals(nick));
    public bool IsExisting(User user) => this.FirstOrDefault(u => u.Nick.Equals(user.Nick)) != null;
}