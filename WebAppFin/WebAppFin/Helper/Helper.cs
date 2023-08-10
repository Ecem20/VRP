using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppFin.Helper
{
	public static class Helper
	{
		public static string admin = "Admin";
		public static string client = "Client";

		public static List<SelectListItem> GetRolesForDropDown()
		{
			return new List<SelectListItem>
			{
				new SelectListItem{Value=Helper.admin,Text=Helper.admin},
				new SelectListItem{Value=Helper.client,Text=Helper.client},
			};
		}
	}
}
