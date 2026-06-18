namespace kursovayaApi.Models
{
    public class SeedData
    {
        public static void SeedDatabase(KursovayaContext context)
        {
            if(context.Users.Count() == 0)
            {
                User user = new User { UserId = 0, Login = "admin123", FullName = "Иванов Иван Иванович", PasswordHash = "hash1", Role = "admin", Department = "Администрация", IsActive = true, CreatedAt = DateTime.Now };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
