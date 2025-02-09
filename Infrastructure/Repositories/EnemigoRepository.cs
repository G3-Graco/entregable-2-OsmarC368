using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EnemigoRepository : BaseRepository<Enemigo>, IEnemigoRepository
    {
        // internal AppDbContext Context;
        // internal DbSet<Enemigo> dbSet;
        public EnemigoRepository(AppDbContext context) : base(context) {
            // context = context;
            // dbSet = context.Set<Enemigo>();
        }

        // public virtual async Task<IEnumerable<Enemigo>> GetByLevel(int level)
        // {
        //     return await dbSet.Include(x => level <= x.nivelAmenaza <= (level+5)).ToListAsync();
        // }
    }
}