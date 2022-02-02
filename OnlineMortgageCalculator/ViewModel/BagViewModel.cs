using OnlineMortgageCalculator.Models;

namespace OnlineMortgageCalculator.ViewModel
{
	public class BagViewModel
	{
		public LoanModel loanModel { get; set; }

		public LoginViewModel LoginViewModel { get; set; }

		public Register RegisterModel { get; set; }

		public RegisterUser RegisterUser { get; set; }
	}
}