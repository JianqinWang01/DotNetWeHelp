using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetWeHelp.Data;
using DotNetWeHelp.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;

namespace DotNetWeHelp.Controllers
{
    //[Authorize]
    public class NewsController : Controller
    {
        private readonly GlobalDBContext _context;
        private readonly IWebHostEnvironment hostingEnviroment;
        public NewsController(GlobalDBContext context,IWebHostEnvironment e)
        {
            _context = context;
            this.hostingEnviroment = e;
        }
        
        // GET: News
        public async Task<IActionResult> Index(int page=1)
        {
            var query = _context.News.AsNoTracking().OrderBy(s => s.CreatedDate);
            var model = await PagingList.CreateAsync(query, 5,page);
            //return View(await _context.News.ToListAsync());
            return View(model);

        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,CreateDate,ImagePath,Description")] News news)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(news);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(news);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create( String Title, String CreateDate, IFormFile ImagePath, String Description)
        {
            News news = new News();
            //CultureInfo culture = new CultureInfo("en-US");
            DateTime createdDate = DateTime.Parse(CreateDate);
            news.Title = Title;
            news.CreateDate = createdDate;
            news.Description = Description;
            string upLoadFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImagePath.FileName);
            string filePath = Path.Combine(upLoadFolder, uniqueFileName);
            news.ImagePath = "/images/"+uniqueFileName;
            using (var stream = System.IO.File.Create(filePath))
            {
                await ImagePath.CopyToAsync(stream);
            }
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);

        }
        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile ImageFile,String oldName,[Bind("Id,Title,CreateDate,ImagePath,Description,CreatedDate,ModifiedDate")] News news)
        {
            Console.WriteLine("Idis:" + id);
            if (id != news.Id)
            {
                return NotFound();
            }

            //var oldNews = await _context.News.FindAsync(id);
            var subOldName = oldName.Substring(oldName.IndexOf('_') + 1);
            Console.WriteLine("Test old ImageName:" + oldName+" subOld Name:"+subOldName);
            if (ImageFile != null)
            {
                 Console.WriteLine("There is image upload");
                    //Delete the old one
                    String upLoadFolder = Path.Combine(hostingEnviroment.WebRootPath);

                    String oldFilePath = upLoadFolder+ oldName;
                Console.WriteLine("oldFullPath:" + oldFilePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                        catch (Exception e)
                        {
                            Console.Write(e.Message);
                        }
                    }
                    
                    //==================Create new one========

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);

                    string filePath = Path.Combine(upLoadFolder, "images",uniqueFileName);
                    news.ImagePath = "/images/" + uniqueFileName;


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
            }
            else
            {
                Console.WriteLine("NotChanged");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            var filePath = hostingEnviroment.WebRootPath + news.ImagePath;
                //Path.Combine(hostingEnviroment.WebRootPath,"",news.ImagePath.Trim());

            Console.WriteLine("happy:"+hostingEnviroment.WebRootPath);
            Console.WriteLine("birthday:" + filePath);
            
            if (System.IO.File.Exists(filePath)) {
                try {
                    System.IO.File.Delete(filePath);
                } catch (Exception e) {
                    Console.Write(e.Message);
                }
            }
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
