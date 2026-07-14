using Microsoft.AspNetCore.Mvc;

namespace ScoreSphere.WebUI.ViewComponents
{
    public class _LayoutNavbarComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
