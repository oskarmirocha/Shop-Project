using Microsoft.EntityFrameworkCore;
using Shop.Data.Base;
using Shop.Data.ViewModels;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Services
{
    public class SongsService:EntityBaseRepository<Song>, ISongsService
    {     
        private readonly AppDbContext _context;
        public SongsService(AppDbContext context):base(context)
        {
             _context = context;
        }

        public async Task AddNewSongAsync(NewSongVM data)
        {
            var newSong = new Song()
            {
                Name = data.Name,
                FullName = data.FullName,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                RecordLabelId = data.RecordLabelId,
                SongCategory = data.SongCategory,
                ProducerId = data.ProducerId
            };
            await _context.Songs.AddAsync(newSong);
            await _context.SaveChangesAsync();

            //Add Song Singer
            foreach(var SingerId in data.SingerIds)
            {
                var newSingerSong = new Singer_Song()
                {
                    SongId = newSong.Id,
                    SingerId = SingerId
                };
                await _context.Singers_Songs.AddAsync(newSingerSong);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<NewSongDropdownsVM> GetNewSongDropdownsValue()
        {
            var response = new NewSongDropdownsVM()
            {
                Singers = await _context.Singers.OrderBy(n => n.FullName).ToListAsync(),
                Labels = await _context.RecordLabels.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };
            return response;

        }

        public async Task<Song> GetSongByIdAsync(int id)
        {
             var songDetails = await _context.Songs
                 .Include(r => r.RecordLabel)
                 .Include(p => p.Producer)
                 .Include(sg => sg.Singers_Songs).ThenInclude(s => s.Singer)
                 .FirstOrDefaultAsync(n => n.Id == id);
             return songDetails;
        }

        public async Task UpdateSongAsync(NewSongVM data)
        {
            var dbSong = await _context.Songs.FirstOrDefaultAsync(n => n.Id == data.Id);
            if(dbSong != null)
            {

                dbSong.Name = data.Name;
                dbSong.FullName = data.FullName;
                dbSong.Price = data.Price;
                dbSong.ImageUrl = data.ImageUrl;
                dbSong.RecordLabelId = data.RecordLabelId;
                dbSong.SongCategory = data.SongCategory;
                dbSong.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }
            //Remove existing Singers
            var existingSingersDb = _context.Singers_Songs.Where(n => n.SongId == data.Id).ToList();
            _context.Singers_Songs.RemoveRange(existingSingersDb);
            await _context.SaveChangesAsync();

            

            //Add Song Singer
            foreach (var SingerId in data.SingerIds)
            {
                var newSingerSong = new Singer_Song()
                {
                    SongId = data.Id,
                    SingerId = SingerId
                };
                await _context.Singers_Songs.AddAsync(newSingerSong);
            }
            await _context.SaveChangesAsync();
        }
    }
}
