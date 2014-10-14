using System.Web.Mvc;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Mvc.Controllers;
using BE.ModelosIII.Tasks.Commands.Account;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Mvc.Areas.Owner.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IMappingEngine _mappingEngine;

        public AccountController(
            ICommandProcessor commandProcessor,
            IMappingEngine mappingEngine)
        {
            _commandProcessor = commandProcessor;
            _mappingEngine = mappingEngine;
        }

        [HttpGet]
        public ActionResult Manage()
        {
            var user = base.GetCurrentUser();
            var command = _mappingEngine.Map<User, ManageAccountCommand>(user);

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageAccountCommand command)
        {
            if (ModelState.IsValid)
            {
                _commandProcessor.Process(command);
            }
            return View(command);
        }
    }
}
