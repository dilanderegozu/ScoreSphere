using Microsoft.AspNetCore.Mvc;

namespace ScoreSphere.WebUI.ViewComponents
{
    public class _LayoutFooterComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
