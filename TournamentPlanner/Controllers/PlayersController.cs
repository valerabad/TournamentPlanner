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

namespace TournamentPlanner.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IClubService clubService;
        private readonly UserManager<DAL.Entities.User> _userManager;

        public PlayersController(
            IPlayerService playerService, 
            IClubService clubService,
            UserManager<DAL.Entities.User> _userManager)
        {
            this.playerService = playerService;
            this.clubService = clubService;
            this._userManager = _userManager;
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

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // ?
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

        public IActionResult ExportToExcel()
        {
            var players = playerService.GetPlayers();

            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Players");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "Sample";
                using (var r = worksheet.Cells["A1:C1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }

                worksheet.Cells["A4"].Value = "Last name";
                worksheet.Cells["B4"].Value = "First name";
                worksheet.Cells["C4"].Value = "Birthday";
                worksheet.Cells["A4:C4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:C4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:C4"].Style.Font.Bold = true;

                row = 5;
                foreach (var player in players)
                {
                    worksheet.Cells[row, 1].Value = player.LastName;
                    worksheet.Cells[row, 2].Value = player.FirstName;
                    worksheet.Cells[row, 3].Value = player.Birthday;

                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Player List";
                xlPackage.Workbook.Properties.Author = "noname";
                xlPackage.Workbook.Properties.Subject = "Player List";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "players.xlsx");
        }

        [HttpGet]
        public IActionResult BatchPlayerUpload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BatchPlayerUpload(IFormFile batchUsers)
        {

            if (ModelState.IsValid)
            {
                if (batchUsers?.Length > 0)
                {
                    var stream = batchUsers.OpenReadStream();
                    List<PlayerViewModel> models = new List<PlayerViewModel>();
                    try
                    {
                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets.First();//package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (var row = 2; row <= rowCount; row++)
                            {
                                try
                                {

                                    var firstName = worksheet.Cells[row, 1].Value?.ToString();
                                    var lastName = worksheet.Cells[row, 2].Value?.ToString();
                                    var birthDay = (DateTime?)worksheet.Cells[row, 3].Value; // ?? (DateTime)worksheet.Cells[row, 3].Value;

                                    var playerViewModel = new PlayerViewModel()
                                    {
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Birthday = birthDay
                                    };

                                    models.Add(playerViewModel);

                                    if (ModelState.IsValid)
                                    {
                                        PlayerDTO playerDTO = new PlayerDTO()
                                        {
                                            //Id = playerViewModel.Id,
                                            FirstName = playerViewModel.FirstName,
                                            AddressId = playerViewModel.AddressId,
                                            Birthday = playerViewModel.Birthday,
                                            EntryMethod = playerViewModel.EntryMethod,
                                            Gender = playerViewModel.Gender,
                                            LastName = playerViewModel.LastName,
                                            Notes = playerViewModel.Notes,
                                            ClubId = playerViewModel.ClubId,
                                        };

                                        playerService.Create(playerDTO);

                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Something went wrong");
                                }
                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception e)
                    {
                        return View();
                    }
                }
            }

            return View();
        }
    }
}
