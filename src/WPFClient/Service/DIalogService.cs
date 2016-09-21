using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Caliburn.Micro;

namespace SuitsupplyTestTask.WPFClient.Service
{
    public class DialogService
    {
        private readonly WindowManager windowManager = new WindowManager();

        private static DialogService current;

        private DialogService()
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
        public bool? ShowDialog(IScreen dialogModel) => windowManager.ShowDialog(dialogModel);


        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
