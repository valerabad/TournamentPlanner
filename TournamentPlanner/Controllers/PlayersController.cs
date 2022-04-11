using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TournamentPlanner.Models;
using BLL.DTO;
using Microsoft.AspNetCore.Identity;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TournamentPlanner.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IClubService clubService;
        private readonly IExcelService excelService;
        private readonly UserManager<DAL.Entities.User> _userManager;

        public PlayersController(
            IPlayerService playerService, 
            IClubService clubService,
            IExcelService excelService,
            UserManager<DAL.Entities.User> _userManager)
        {
            this.playerService = playerService;
            this.clubService = clubService;
            this._userManager = _userManager;
            this.excelService = excelService;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var players = playerService.GetPlayers().Select(x => new PlayerViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                EntryMethod = x.EntryMethod,
                AddressId = x.AddressId,
                Birthday = x.Birthday,
                Gender = x.Gender,
                Notes = x.Notes,
                Id = x.Id
            });
            return View(players);
        }

        
        public async Task<IActionResult> Details(int? id) // asynk await
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = playerService.GetPlayer(id);
            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                AddressId = player.AddressId,
                Birthday = player.Birthday,
                ClubId = player.ClubId,
                EntryMethod = player.EntryMethod,
                Gender = player.Gender,
                LastName = player.LastName,
                Notes = player.Notes,
                //Clubs = model
            };
            if (player == null)
            {
                return NotFound();
            }

            return View(playerViewModel);
        }

        [Authorize(Roles = "admin, player, guest")]
        public IActionResult Create()
        {
            // We have not use DAL here, but just for test 
            var clubList = clubService.GetClubs().Select(x => new ClubViewModel
            {
                Logo = x.Logo,
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            });

            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Clubs = clubList
            };

            return View(playerViewModel);
        }

        [Authorize(Roles = "admin, player")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerViewModel player)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("player"))
                await _userManager.AddToRoleAsync(user, "player");
            
            if (ModelState.IsValid)
            {
                PlayerDTO playerDTO = new PlayerDTO()
                {
                    Id = player.Id,
                    FirstName = player.FirstName,
                    AddressId = player.AddressId,
                    Birthday = player.Birthday,
                    EntryMethod = player.EntryMethod,
                    Gender = player.Gender,
                    LastName = player.LastName,
                    Notes = player.Notes,
                    ClubId = player.ClubId,
                    UserId = user.Id,
                  
                };
               
                playerService.Create(playerDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        [Authorize(Roles = "admin, player")]
        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }


            var avaliableClubs = clubService.GetClubs().Select(x => new ClubViewModel()
            {
                Id = x.Id,
                Logo = x.Logo,
                Description = x.Description,
                Title = x.Title
            });

            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                AddressId = player.AddressId,
                Birthday = player.Birthday,
                ClubId = player.ClubId,
                EntryMethod = player.EntryMethod,
                Gender = player.Gender,
                LastName = player.LastName,
                Notes = player.Notes,
                Clubs = avaliableClubs
            };
            return View(playerViewModel);
        }

        [Authorize(Roles = "admin, player")]
        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlayerViewModel player)
        {
            if (id != player.Id)
            {
                return NotFound();  
            }

            if (ModelState.IsValid)
            {
                try
                {
                    PlayerDTO playerDTO = new PlayerDTO()
                    {
                        Id = player.Id,
                        FirstName = player.FirstName,
                        AddressId = player.AddressId,
                        Birthday = player.Birthday,
                        ClubId = player.ClubId,
                        EntryMethod = player.EntryMethod,
                        Gender = player.Gender,
                        LastName = player.LastName,
                        Notes = player.Notes
                    };
                    playerService.Edit(playerDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (playerService.GetPlayer(player.Id) == null) // or PlayerExists ?
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = playerService.GetPlayer(id);

            if (player == null)
            {
                return NotFound();
            }

            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                AddressId = player.AddressId,
                Birthday = player.Birthday,
                ClubId = player.ClubId,
                EntryMethod = player.EntryMethod,
                Gender = player.Gender,
                LastName = player.LastName,
                Notes = player.Notes,
                //Clubs = new List<ClubViewModel> { clubViewModel}

            };
            return View(playerViewModel);
        }

        [Authorize(Roles = "admin")]
        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            playerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportToExcel()
        {
            string workSheetName = "Players";
            var players = playerService.GetPlayers();
            var outputStream = excelService.ExportToExcel(players, workSheetName);
            return File(outputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workSheetName}.xlsx");
        }

        [HttpGet]
        public IActionResult BatchPlayerUpload()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BatchPlayerUpload(IFormFile batchPlayers)
        {

            if (ModelState.IsValid)
            {
                if (batchPlayers?.Length > 0)
                {
                    var stream = batchPlayers.OpenReadStream();
                    excelService.ImportFromExcel(stream);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
