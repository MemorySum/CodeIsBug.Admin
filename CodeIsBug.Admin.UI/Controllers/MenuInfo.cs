using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeIsBug.Admin.Models;
using CodeIsBug.Admin.Models.DbContext;

namespace CodeIsBug.Admin.UI.Controllers
{
    public class MenuInfo : Controller
    {
        private readonly CodeIsBugContext _context;

        public MenuInfo(CodeIsBugContext context)
        {
            _context = context;
        }

        // GET: MenuInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.ESysMenus.ToListAsync());
        }

        // GET: MenuInfo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSysMenu = await _context.ESysMenus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (eSysMenu == null)
            {
                return NotFound();
            }

            return View(eSysMenu);
        }

        // GET: MenuInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,ParentId,Name,Url,Icon,Sort,Level,IsDeleted,AddTime,ModifyTime")] ESysMenu eSysMenu)
        {
            if (ModelState.IsValid)
            {
                eSysMenu.MenuId = Guid.NewGuid();
                _context.Add(eSysMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eSysMenu);
        }

        // GET: MenuInfo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSysMenu = await _context.ESysMenus.FindAsync(id);
            if (eSysMenu == null)
            {
                return NotFound();
            }
            return View(eSysMenu);
        }

        // POST: MenuInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MenuId,ParentId,Name,Url,Icon,Sort,Level,IsDeleted,AddTime,ModifyTime")] ESysMenu eSysMenu)
        {
            if (id != eSysMenu.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eSysMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ESysMenuExists(eSysMenu.MenuId))
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
            return View(eSysMenu);
        }

        // GET: MenuInfo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eSysMenu = await _context.ESysMenus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (eSysMenu == null)
            {
                return NotFound();
            }

            return View(eSysMenu);
        }

        // POST: MenuInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var eSysMenu = await _context.ESysMenus.FindAsync(id);
            _context.ESysMenus.Remove(eSysMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ESysMenuExists(Guid id)
        {
            return _context.ESysMenus.Any(e => e.MenuId == id);
        }
    }
}
