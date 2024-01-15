namespace GelionTransApi.Controllers;

using LibraryAPI.Domain.Entities.Auth;
using Microsoft.AspNetCore.Mvc;

[Controller]
public abstract class BaseController : ControllerBase
{
    public new User User => (User?)HttpContext.Items["Account"] ?? throw new ApplicationException("Invalid User");

    public bool IsAnonymousSession => HttpContext.Items["Account"] == null;
}