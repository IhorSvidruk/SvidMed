using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Common;
using SvidMed.Web.Controllers;

namespace SvidMed.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
