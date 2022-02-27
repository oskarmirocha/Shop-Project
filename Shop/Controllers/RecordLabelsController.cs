using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class RecordLabelsController : Controller
    {

        private readonly IRecordLabelsService _service;

        public RecordLabelsController(IRecordLabelsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allRecordLabels = await _service.GetAllAsync();
            return View(allRecordLabels);
        }

        //GET: RecordLabel/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]RecordLabel recordlabel)
        {
            if (!ModelState.IsValid) return View(recordlabel);
            await _service.AddAsync(recordlabel);
            return RedirectToAction(nameof(Index));
        }

        //GET: RecordLabel/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var recordlabelsDetails = await _service.GetByIdAsync(id);
            if (recordlabelsDetails == null) return View("NotFound");
            return View(recordlabelsDetails);
        }

        //GET: RecordLabel/Edit/1

        public async Task<IActionResult> Edit(int id)
        {
            var recordlabelsDetails = await _service.GetByIdAsync(id);
            if (recordlabelsDetails == null) return View("NotFound");
            return View(recordlabelsDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] RecordLabel recordlabel)
        {
            if (!ModelState.IsValid) return View(recordlabel);
            await _service.UpdateAsync(id, recordlabel);
            return RedirectToAction(nameof(Index));
        }

        //GET: RecordLabel/Delete/1

        public async Task<IActionResult> Delete(int id)
        {
            var recordlabelsDetails = await _service.GetByIdAsync(id);
            if (recordlabelsDetails == null) return View("NotFound");
            return View(recordlabelsDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var recordlabelsDetails = await _service.GetByIdAsync(id);
            if (recordlabelsDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
