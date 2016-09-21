﻿using System.Windows;
using System.Windows.Data;

namespace SuitsupplyTestTask.WPFClient.Helpers
{
    /// <summary>
    /// Interaction logic for ChooseFileControl.xaml
    /// </summary>
    public partial class ChooseFileControl
    {
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
            "Path", typeof(string), typeof(ChooseFileControl), new FrameworkPropertyMetadata(default(string))
                                                               {
                                                                   BindsTwoWayByDefault = true,
                                                                   DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                                                               });

        public ChooseFileControl()
        {
            InitializeComponent();
        }

        public string Path { get { return (string)GetValue(PathProperty); } set { SetValue(PathProperty, value); } }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                TextBox.Text = filename;
            }
        }
    }
}