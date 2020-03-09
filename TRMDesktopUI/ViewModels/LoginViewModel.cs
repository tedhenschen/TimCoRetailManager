using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel: Screen
    {
		private string _password;
		private string _username;
		private IAPIHelper _apiHelper;
		private bool _isErrorVisible;

		private string _errorMessage;

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set {
				_errorMessage = value;
				NotifyOfPropertyChange(() => IsErrorVisible);
				NotifyOfPropertyChange(() => ErrorMessage);
			}
		}


		public LoginViewModel(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}
		public string Username
		{
			get { return _username; }
			set 
			{
				_username = value;
				NotifyOfPropertyChange(() => Username);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}
		public bool CanLogIn
		{
			get
			{
				bool output = false;
				if (Username?.Length > 0 && Password?.Length > 0)
				{
					output = true;
				}
				return output;
			}
		}
		public async Task LogIn()
		{
			try
			{
				ErrorMessage = "";
				var result = await _apiHelper.Authenticate(Username, Password);
				
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}
		public bool IsErrorVisible
		{
			get {
				bool output = false;
				if (ErrorMessage?.Length > 0)
				{
					output = true;
				}
				return output; }

		}




	}
}
