using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutbalnetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

        private void FeedbackSendClicked(object sender, EventArgs e)
        {
            try
            {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vitvasakport@gmail.com");
                mail.To.Add("vitvasak@outlook.com");
                mail.Subject = "FutbalVille-Feedback";
                mail.Body = FeedbackEditor.Text;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vitvasakport@gmail.com", "vvport1598");

                SmtpServer.Send(mail);
                DisplayAlert("Odoslané", "Ďakujeme za váš názor", "OK");

            }
            catch (Exception)
            {
                DisplayAlert("Chyba", "Odosielanie zlyhalo", "OK");
            }
        }
        private void BugSendClicked(object sender, EventArgs e)
		{
            try
            {
                var mail = new MailMessage();
                var SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vitvasakport@gmail.com");
                mail.To.Add("vitvasak@outlook.com");
                mail.Subject = "FutbalVille-Bug";
                mail.Body = BugEditor.Text;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vitvasakport@gmail.com", "vvport1598");

                SmtpServer.Send(mail);
                DisplayAlert("Odoslané", "Ďakujeme za upozornenie na problém", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Chyba", "Odosielanie zlyhalo", "OK");
            }
        }
	}
}