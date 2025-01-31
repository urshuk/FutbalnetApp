﻿using FutbalnetApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutbalnetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		SettingsViewModel viewModel;
		public SettingsPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new SettingsViewModel();
		}

		private void FeedbackSendClicked(object sender, EventArgs e)
		{
			try
			{
				var mail = new MailMessage();
				var SmtpServer = new SmtpClient("smtp.gmail.com");

				mail.From = new MailAddress("vitvasakport@gmail.com");
				mail.To.Add("vitvasak@outlook.com");
				mail.IsBodyHtml = true;
				mail.Subject = "FutbalVille-Feedback";
				mail.Body = FeedbackEditor.Text;

				SmtpServer.Port = 587;
				SmtpServer.Host = "smtp.gmail.com";
				SmtpServer.EnableSsl = true;
				SmtpServer.UseDefaultCredentials = false;
				SmtpServer.Credentials = new System.Net.NetworkCredential("vitvasakport@gmail.com", "vvport1598");

				SmtpServer.Send(mail);
				FeedbackEditor.Text = "";
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
				var deviceInfo = $"<br/><br/><br/>Platform: {DeviceInfo.Platform}<br/>" +
					$"Idiom: {DeviceInfo.Idiom}<br/>" +
					$"Version: {DeviceInfo.VersionString}<br/>" +
					$"Model: {DeviceInfo.Model}<br/>" +
					$"Manufacturer: {DeviceInfo.Manufacturer}<br/>" +
					$"Device name: {DeviceInfo.Name}<br/>" +
					$"Type: {DeviceInfo.DeviceType}<br/>" +
					$"App Version: {VersionTracking.CurrentVersion}";

				var mail = new MailMessage();
				var SmtpServer = new SmtpClient("smtp.gmail.com");

				mail.From = new MailAddress("vitvasakport@gmail.com");
				mail.To.Add("vitvasak@outlook.com");
				mail.IsBodyHtml = true;
				mail.Subject = "FutbalVille-Bug";
				mail.Body = BugEditor.Text + deviceInfo;

				SmtpServer.Port = 587;
				SmtpServer.Host = "smtp.gmail.com";
				SmtpServer.EnableSsl = true;
				SmtpServer.UseDefaultCredentials = false;
				SmtpServer.Credentials = new System.Net.NetworkCredential("vitvasakport@gmail.com", "vvport1598");

				SmtpServer.Send(mail);
				BugEditor.Text = "";
				DisplayAlert("Odoslané", "Ďakujeme za upozornenie na problém", "OK");
			}
			catch (Exception)
			{
				DisplayAlert("Chyba", "Odosielanie zlyhalo", "OK");
			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadSettingsCommand.Execute(null);
		}

		private void BugEditor_TextChanged(object sender, TextChangedEventArgs e) => BugSendButton.IsEnabled = e.NewTextValue.Length > 2;
		private void FeedbackEditor_TextChanged(object sender, TextChangedEventArgs e) => FeedbackSendButton.IsEnabled = e.NewTextValue.Length > 2;

		private void minutePicker_Unfocused(object sender, FocusEventArgs e)
		{
			viewModel.SaveSettingsCommand.Execute(null);
		}
		private void SwitchCell_OnChanged(object sender, ToggledEventArgs e)
		{
			viewModel.SaveSettingsCommand.Execute(null);
		}

		private void ViewCell_Tapped(object sender, EventArgs e)
		{
			minutePicker.Focus();
		}

		private async void GoToFVButton_Clicked(object sender, EventArgs e)
		{
			await Browser.OpenAsync("https://futbalville.sk/", BrowserLaunchMode.SystemPreferred);
		}
	}
}