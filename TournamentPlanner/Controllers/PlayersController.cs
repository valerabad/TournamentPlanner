using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TournamentPlanner.Models;

namespace TournamentPlanner.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService;
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

    //    // GET: Players/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var player = await playerService.Player
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (player == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(player);
    //    }

    //    // GET: Players/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Players/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(PlayerViewModel player)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var dbPlayer = new DbPlayer()
    //            {
    //                Id = player.Id,
    //                FirstName = player.FirstName,
    //                LastName = player.LastName,
    //                Gender = player.Gender,
    //                Birthday = player.Birthday,
    //                AddressId = player.AddressId,
    //                ClubId = player.ClubId
    //            };

    //            //_context.Add(dbPlayer);
    //            playerService.Add(player);
    //            await playerService.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(player);
    //    }

    //    // GET: Players/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var player = await playerService.Player.FindAsync(id);
    //        if (player == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(player);
    //    }

    //    // POST: Players/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, PlayerViewModel player)
    //    {
    //        if (id != player.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                playerService.Update(player);
    //                await playerService.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!PlayerExists(player.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(player);
    //    }

    //    // GET: Players/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var player = await playerService.Player
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (player == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(player);
    //    }

    //    // POST: Players/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var player = await playerService.Player.FindAsync(id);
    //        playerService.Player.Remove(player);
    //        await playerService.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool PlayerExists(int id)
    //    {
    //        return playerService.Player.Any(e => e.Id == id);
    //    }
    }
}
