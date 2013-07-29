namespace Epic.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }

        // This is only needed for validation; I don't like doing it this this way
        public string Username { get; set; }
    }
}