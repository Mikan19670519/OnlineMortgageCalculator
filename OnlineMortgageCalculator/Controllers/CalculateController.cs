using OnlineMortgageCalculator.ViewModel;
using System.Web.Mvc;

namespace OnlineMortgageCalculator.Controllers
{
	public class CalculateController : Controller
	{
		public ActionResult Index()
		{
			LoanModel model = new LoanModel();
			return View(model);
		}
	}
}