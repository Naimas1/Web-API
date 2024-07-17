using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoWebApi.Data;
using InfoWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    private readonly AppDbContext _context;

    public InfoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Info>>> GetInfos()
    {
        return await _context.Infos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Info>> GetInfo(int id)
    {
        var info = await _context.Infos.FindAsync(id);
        if (info == null)
        {
            return NotFound();
        }
        return info;
    }

    [HttpPost]
    public async Task<ActionResult<Info>> PostInfo(Info info)
    {
        _context.Infos.Add(info);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInfo), new { id = info.Id }, info);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutInfo(int id, Info info)
    {
        if (id != info.Id)
        {
            return BadRequest();
        }

        _context.Entry(info).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InfoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInfo(int id)
    {
        var info = await _context.Infos.FindAsync(id);
        if (info == null)
        {
            return NotFound();
        }

        _context.Infos.Remove(info);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool InfoExists(int id)
    {
        return _context.Infos.Any(e => e.Id == id);
    }
}
