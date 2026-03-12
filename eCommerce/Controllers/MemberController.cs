using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace eCommerce.Controllers;

public class MemberController : Controller
{
    public IActionResult Register()
    {
        return View();
    }
}
