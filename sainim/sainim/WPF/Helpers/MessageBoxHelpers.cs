using sainim.Models.Extensions;
using System.Windows;

namespace sainim.WPF.Helpers
{
    public class MessageBoxHelpers
    {
        public void ShowGenericErrorMessageBox(string errorStatement, string rawErrorMessage)
        {
            MessageBox.Show($"{errorStatement}\n{"ErrorMessage".Resource()}\n{rawErrorMessage}",
                               "Error".Resource(),
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
        }
    }
}