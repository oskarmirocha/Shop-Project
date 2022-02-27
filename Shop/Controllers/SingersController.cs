using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Data.Services;
using Shop.Data.Static;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SingersController : Controller
    {
        private readonly ISingersService _service;

        public SingersController(ISingersService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Singers/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")]Singer singer)
        {
            if (!ModelState.IsValid)
            {
                return View(singer);
            }
            await _service.AddAsync(singer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Singers/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var singerDetails = await _service.GetByIdAsync(id);

            if (singerDetails == null) return View("NotFound");
            return View(singerDetails);
        }

        //Get: Singers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var singerDetails = await _service.GetByIdAsync(id);

            if (singerDetails == null) return View("NotFound");
            return View(singerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,ProfilePictureUrl,Bio")] Singer singer)
        {
            if (!ModelState.IsValid)
            {
                return View(singer);
            }
            await _service.UpdateAsync(id, singer);
            return RedirectToAction(nameof(Index));
        }

        //Get: Singers/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var singerDetails = await _service.GetByIdAsync(id);

            if (singerDetails == null) return View("NotFound");
            return View(singerDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var singerDetails = await _service.GetByIdAsync(id);
            if (singerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
