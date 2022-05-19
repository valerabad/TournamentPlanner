﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.DTO;
using BLL.Interfaces;
using TournamentPlanner.Models;
using Microsoft.AspNetCore.Identity;

namespace TournamentPlanner.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ITournamentService tourService;
        private readonly IPlayerService playerService;
        private readonly UserManager<DAL.Entities.User> userManager;

        public TournamentsController(ITournamentService _tournamentService, IPlayerService _playerService, UserManager<DAL.Entities.User> _userManager)
        {
            tourService = _tournamentService;
            playerService = _playerService;
            userManager = _userManager;
        }

        // GET: Tournaments
        public async Task<IActionResult> Index()
        {
            var tourViewModel = tourService.GetAll().Select(x => new TournamentViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Email = x.Email,
                WebSite = x.WebSite,
                Logo = x.Logo,
                DateEnd = x.DateEnd,
                DateStart = x.DateStart,
                EntryMethod = x.EntryMethod,
                Events = x.Events,
                CourtsCount = x.CourtsCount
            });
            return View(tourViewModel);

        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = tourService.GetTourById(id);

            if (tour == null)
            {
                return NotFound();
            }

            TournamentViewModel tournamentViewModel = new TournamentViewModel()
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Email = tour.Email,
                WebSite = tour.WebSite,
                Logo = tour.Logo,
                DateEnd = tour.DateEnd,
                DateStart = tour.DateStart,
                EntryMethod = tour.EntryMethod,
                Events = tour.Events,
                CourtsCount = tour.CourtsCount
            };

            return View(tournamentViewModel);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TournamentViewModel tour)
        {
            if (ModelState.IsValid)
            {
                var tourDTO = new TournamentDTO()
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Description = tour.Description,
                    Email = tour.Email,
                    WebSite = tour.WebSite,
                    Logo = tour.Logo,
                    DateEnd = tour.DateEnd,
                    DateStart = tour.DateStart,
                    EntryMethod = (BLL.DTO.EntryMethodEnum)tour.EntryMethod,
                    Events = tour.Events,
                    CourtsCount = tour.CourtsCount
                };

                tourService.Create(tourDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = tourService.GetTourById(id);

            if (tour == null)
            {
                return NotFound();
            }

            var tournamentViewModel = new TournamentViewModel()
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Email = tour.Email,
                WebSite = tour.WebSite,
                Logo = tour.Logo,
                DateEnd = tour.DateEnd,
                DateStart = tour.DateStart,
                EntryMethod = tour.EntryMethod,
                Events = tour.Events,
                CourtsCount = tour.CourtsCount
            };

            return View(tournamentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Events,CourtsCount")] TournamentViewModel tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tourDTO = new TournamentDTO()
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Description = tour.Description,
                    Email = tour.Email,
                    WebSite = tour.WebSite,
                    Logo = tour.Logo,
                    DateEnd = tour.DateEnd,
                    DateStart = tour.DateStart,
                    EntryMethod = tour.EntryMethod,
                    Events = tour.Events,
                    CourtsCount = tour.CourtsCount
                };
                try
                {
                    tourService.Edit(tourDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(tour.Id))
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
            return View(tour);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = tourService.GetTourById(id);
            if (tour == null)
            {
                return NotFound();
            }

            TournamentViewModel tournamentViewModel = new TournamentViewModel()
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                Email = tour.Email,
                WebSite = tour.WebSite,
                Logo = tour.Logo,
                DateEnd = tour.DateEnd,
                DateStart = tour.DateStart,
                EntryMethod = tour.EntryMethod,
                Events = tour.Events,
                CourtsCount = tour.CourtsCount
            };

            return View(tournamentViewModel);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = tourService.GetTourById(id);
            tourService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(int id)
        {
            return tourService.GetAll().Any(e => e.Id == id);
        }

        public async Task<IActionResult> EnabledTournamentsList()
        {
            var tourViewModel = tourService.GetByActualDate()
                .Select(x => new TournamentViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Email = x.Email,
                    WebSite = x.WebSite,
                    Logo = x.Logo,
                    DateEnd = x.DateEnd,
                    DateStart = x.DateStart,
                    EntryMethod = x.EntryMethod,
                    Events = x.Events,
                    CourtsCount = x.CourtsCount
                });
            return View(tourViewModel);
        }

        public async Task<IActionResult> AddPlayerToTour(int? id)
        {
            var currTour = tourService.GetTourById(id);

            TournamentViewModel tournamentViewModel = new TournamentViewModel()
            {
                Id = currTour.Id,
                Name = currTour.Name,
                Description = currTour.Description,
                Email = currTour.Email,
                WebSite = currTour.WebSite,
                Logo = currTour.Logo,
                DateEnd = currTour.DateEnd,
                DateStart = currTour.DateStart,
                EntryMethod = currTour.EntryMethod,
                Events = currTour.Events,
                CourtsCount = currTour.CourtsCount
            };

            var user = await userManager.FindByEmailAsync(User.Identity.Name);

            var currPlayer = playerService.GetPlayers().FirstOrDefault(x => x.UserId == user.Id);


            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                Id = currPlayer.Id,
                ClubId = currPlayer.ClubId,
                FirstName = currPlayer.FirstName,
                LastName = currPlayer.LastName,
                EntryMethod = currPlayer.EntryMethod,
                AddressId = currPlayer.AddressId,
                Birthday = currPlayer.Birthday,
                Gender = currPlayer.Gender,
                Notes = currPlayer.Notes
            };

            PlayerTournamentViewModel playerTournamentViewModel = new PlayerTournamentViewModel()
            {
                Player = playerViewModel,
                Tournament = tournamentViewModel
            };

            return View(playerTournamentViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayerToTour(PlayerTournamentViewModel playerTournamentViewModel)
        {

            return View();
        }
    }
}
