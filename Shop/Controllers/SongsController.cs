using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class SongsController : Controller
    {
        private readonly ISongsService _service;

        public SongsController(ISongsService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allSongs = await _service.GetAllAsync(n => n.RecordLabel);
            return View(allSongs);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allSongs = await _service.GetAllAsync(n => n.RecordLabel);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allSongs.Where(n => n.Name.Contains(searchString) || n.FullName.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }


            return View("Index", allSongs);
        }

        //GET: Songs/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var songDetail = await _service.GetSongByIdAsync(id);
            return View(songDetail);
        }

        //GET: Songs/Create
        public async Task<IActionResult> Create()
        {
            var songDropdownsData = await _service.GetNewSongDropdownsValue();

            ViewBag.Labels = new SelectList(songDropdownsData.Labels, "Id", "Name");
            ViewBag.Producers = new SelectList(songDropdownsData.Producers, "Id", "FullName");
            ViewBag.Singers = new SelectList(songDropdownsData.Singers, "Id", "FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewSongVM song)
        {
            if(!ModelState.IsValid)
            {
                var songDropdownsData = await _service.GetNewSongDropdownsValue();
                ViewBag.Labels = new SelectList(songDropdownsData.Labels, "Id", "Name");
                ViewBag.Producers = new SelectList(songDropdownsData.Producers, "Id", "FullName");
                ViewBag.Singers = new SelectList(songDropdownsData.Singers, "Id", "FullName");
                return View(song);
            }
            await _service.AddNewSongAsync(song);
            return RedirectToAction(nameof(Index));
        }

        //GET: Songs/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var songDetails = await _service.GetSongByIdAsync(id);
            if (songDetails == null) return View("NotFound");

            var response = new NewSongVM()
            {
                Id=songDetails.Id,
                Name = songDetails.Name,
                FullName = songDetails.FullName,
                Price = songDetails.Price,
                ImageUrl = songDetails.ImageUrl,
                RecordLabelId = songDetails.RecordLabelId,
                SongCategory = songDetails.SongCategory,
                ProducerId = songDetails.ProducerId,
                SingerIds = songDetails.Singers_Songs.Select(n=>n.SingerId).ToList(),
            };

            var songDropdownsData = await _service.GetNewSongDropdownsValue();

            ViewBag.Labels = new SelectList(songDropdownsData.Labels, "Id", "Name");
            ViewBag.Producers = new SelectList(songDropdownsData.Producers, "Id", "FullName");
            ViewBag.Singers = new SelectList(songDropdownsData.Singers, "Id", "FullName");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewSongVM song)
        {
            if (id != song.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var songDropdownsData = await _service.GetNewSongDropdownsValue();
                ViewBag.Labels = new SelectList(songDropdownsData.Labels, "Id", "Name");
                ViewBag.Producers = new SelectList(songDropdownsData.Producers, "Id", "FullName");
                ViewBag.Singers = new SelectList(songDropdownsData.Singers, "Id", "FullName");
                return View(song);
            }
            await _service.UpdateSongAsync(song);
            return RedirectToAction(nameof(Index));
        }
    }
}
