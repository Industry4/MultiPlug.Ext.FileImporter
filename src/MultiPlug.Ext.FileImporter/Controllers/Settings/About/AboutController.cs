using System.Reflection;
using MultiPlug.Base.Attribute;
using MultiPlug.Base.Http;
using MultiPlug.Ext.FileImporter.Controllers.Shared;

namespace MultiPlug.Ext.FileImporter.Controllers.Settings.About
{
	[Route("about")]
	public class AboutController : SettingsApp
	{
		public Response Get()
		{
			var ExecutingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			return new Response
			{
				Model = new Models.Settings.About.About
				{
					Title = ExecutingAssembly.GetCustomAttribute<AssemblyTitleAttribute>().Title,
					Description = ExecutingAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,
					Company = ExecutingAssembly.GetCustomAttribute<AssemblyCompanyAttribute>().Company,                                   
					Product = ExecutingAssembly.GetCustomAttribute<AssemblyProductAttribute>().Product,
                    Copyright = ExecutingAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright,
                    Trademark = ExecutingAssembly.GetCustomAttribute<AssemblyTrademarkAttribute>().Trademark,
                    Version = ExecutingAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version,
                },
				Template = Templates.SettingsAbout,
			};
		}
	}
}
