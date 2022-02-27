using Shop.Data.Base;
using Shop.Data.ViewModels;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Services
{
    public interface ISongsService:IEntityBaseRepository<Song>
    {
        Task<Song> GetSongByIdAsync(int id);
        Task<NewSongDropdownsVM> GetNewSongDropdownsValue();
        Task AddNewSongAsync(NewSongVM data);
        Task UpdateSongAsync(NewSongVM data);
    }
}
