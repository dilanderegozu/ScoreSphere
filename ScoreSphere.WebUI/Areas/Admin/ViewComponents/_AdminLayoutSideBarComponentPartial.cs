using Microsoft.AspNetCore.Mvc;

namespace ScoreSphere.WebUI.Areas.Admin.ViewComponents
{
    public class _AdminLayoutSideBarComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
