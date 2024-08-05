namespace CsvWinFormsApp.Utilities
{
    internal static class MessagesHelper
    {
        public static DialogResult ShowErrorMessage(string message)
        {
           return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowInfoMessage(string message)
        {
           return MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarningMessage(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
           return MessageBox.Show(message, "Warning", buttons, MessageBoxIcon.Warning);
        }
    }
}
