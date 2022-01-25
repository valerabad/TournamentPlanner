﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TournamentPlanner.Models;
using BLL.DTO;

namespace TournamentPlanner.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IClubService clubService;

        public PlayersController(IPlayerService playerService, IClubService clubService)
        {
            this.playerService = playerService;
            this.clubService = clubService;
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

        // GET: Players/Details/5
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

        //    // GET: Players/Create
        public IActionResult Create()
        {
            // We have not use DAL here, but just for test 
            var clubList = clubService.GetClubs().Select(x => new ClubViewModel
            {
                Logo = x.Logo,
                //Id = x.Id,
                Title = x.Title,
                Description = x.Description
            });

            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Clubs = clubList
            };

            return View(playerViewModel);
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // ?
        public async Task<IActionResult> Create(PlayerViewModel player)
        {
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
                    ClubId = player.ClubId
                };
               
                playerService.Create(playerDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

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

            //ClubDTO clubDTO = clubService.GetClub(player.ClubId);
            //ClubViewModel clubViewModel = new ClubViewModel()
            //{
            //    Id = clubDTO.Id,
            //    Logo = clubDTO.Logo,
            //    Description = clubDTO.Description,
            //    Title = clubDTO.Title
            //};

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

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            playerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //    private bool PlayerExists(int id)
        //    {
        //        return playerService.Player.Any(e => e.Id == id);
        //    }
    }
}
