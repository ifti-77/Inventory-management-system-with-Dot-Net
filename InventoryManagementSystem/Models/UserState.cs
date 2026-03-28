namespace InventoryManagementSystem.Models
{
    public class UserState
    {
        public static bool IsLoggedIn { set; get; } = false;
        public static decimal UserID { set; get; } = 0;
        public static string UserName { set; get; } = string.Empty;
        public static string UserRole { set; get; } = string.Empty;

        public UserState()
        {

        }
    }
}
