using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace OnlineMortgageCalculator.ViewModel
{
	public class LoanModel
	{
		public decimal LoanAmount { get; set; }

		public int InterestRate { get; set; }

		public int LoanTerm { get; set; }

		public decimal DownPayment { get; set; }

		public decimal MonthlyPayment { get; set; }

		public decimal YearlyPayment { get; set; }

		[DisplayName("Terms")]
		public List<SelectListItem> SelectedTerms { get; set; }

		public IEnumerable<SelectListItem> Terms { get; set; }
	}
}