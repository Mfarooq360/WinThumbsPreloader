using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinThumbsPreloader
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения. (The main entry point for the application.)
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            /*
            //Test culture
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            */
            Options options = new Options(arguments);
            if (options.badArguments)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new AboutForm());
            }
            else
            {
                new ThumbnailsPreloader(options.path, options.includeNestedDirectories, options.silentMode, options.multithreaded, options.extensions, options.threadCount, options.thumbnailSizes);
                Application.Run();
            }
        }
        public static void OpenFormCentered(this Form currentForm, Form newForm)
        {
            currentForm.Hide();
            newForm.FormClosed += (s, args) =>
            {
                currentForm.Location = new Point(newForm.Location.X + (newForm.Width - currentForm.Width) / 2,
                                                 newForm.Location.Y + (newForm.Height - currentForm.Height) / 2);
                currentForm.Show();
            };

            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = new Point(currentForm.Location.X + (currentForm.Width - newForm.Width) / 2,
                                         currentForm.Location.Y + (currentForm.Height - newForm.Height) / 2);
            newForm.Owner = currentForm;
            newForm.ShowDialog();
        }

        public static void OpenSecondaryFormCentered(this Form currentForm, Form newForm)
        {
            newForm.FormClosed += (s, args) =>
            {
                currentForm.Focus();
            };

            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = new Point(currentForm.Location.X + (currentForm.Width - newForm.Width) / 2,
                                         currentForm.Location.Y + (currentForm.Height - newForm.Height) / 2);
            newForm.Owner = currentForm;
            newForm.Show();
        }
    }
}