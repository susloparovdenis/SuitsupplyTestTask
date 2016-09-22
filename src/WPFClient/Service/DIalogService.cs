using System;
using System.Windows;

using Caliburn.Micro;

namespace SuitsupplyTestTask.WPFClient.Service
{
    public class DialogService
    {
        private static DialogService current;

        private readonly WindowManager windowManager = new WindowManager();

        protected DialogService()
        {
        }

        public static DialogService Current
        {
            get { return current ?? (current = new DialogService()); }
            set
            {
                if (current != null)
                    throw new InvalidOperationException();
                current = value;
            }
        }

        public virtual bool? ShowDialog(IScreen dialogModel) => windowManager.ShowDialog(dialogModel);

        public virtual void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}