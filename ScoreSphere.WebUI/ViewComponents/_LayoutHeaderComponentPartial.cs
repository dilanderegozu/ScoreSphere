using Microsoft.AspNetCore.Mvc;

namespace ScoreSphere.WebUI.ViewComponents
{
    public class _LayoutHeaderComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}