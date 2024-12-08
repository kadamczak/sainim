using sainim.WPF.Bases;
using System.Diagnostics;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class OpenCreditsCommand : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object? parameter)
        {
            string fileName = @"Resources\Pages\Credits\credits.html";

            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true
            });
        }
    }
}