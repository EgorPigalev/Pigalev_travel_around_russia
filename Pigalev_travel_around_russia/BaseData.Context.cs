﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pigalev_travel_around_russia
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseData : DbContext
    {
        public BaseData()
            : base("name=BaseData")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<HotelComment> HotelComment { get; set; }
        public virtual DbSet<HotelImage> HotelImage { get; set; }
        public virtual DbSet<HotelOfTour> HotelOfTour { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<TypeOfTour> TypeOfTour { get; set; }
    }
}
