using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelAPI.Models
{
    public partial class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Cleaning> Cleaning { get; set; }
        public virtual DbSet<CleaningEquipment> CleaningEquipment { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Dish> Dish { get; set; }
        public virtual DbSet<DishFood> DishFood { get; set; }
        public virtual DbSet<Doljnost> Doljnost { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Fridge> Fridge { get; set; }
        public virtual DbSet<HistorySotrudnik> HistorySotrudnik { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuDate> MenuDate { get; set; }
        public virtual DbSet<MenuDish> MenuDish { get; set; }
        public virtual DbSet<Nomer> Nomer { get; set; }
        public virtual DbSet<PriemPitaniya> PriemPitaniya { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Sklad> Sklad { get; set; }
        public virtual DbSet<Sotrudnik> Sotrudnik { get; set; }
        public virtual DbSet<SotrudnikDoljnost> SotrudnikDoljnost { get; set; }
        public virtual DbSet<TypeMeal> TypeMeal { get; set; }
        public virtual DbSet<TypeNomer> TypeNomer { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-CFGGEN2R\\SQLEXPRESS;Database=Hotel;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("PK_20");

                entity.HasIndex(e => e.ClientId)
                    .HasName("FK_2");

                entity.HasIndex(e => e.NomerId)
                    .HasName("FK_3");

                entity.Property(e => e.IdBook).HasColumnName("ID_Book");

                entity.Property(e => e.ClientId).HasColumnName("Client_ID");

                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.NomerId).HasColumnName("Nomer_ID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_11");

                entity.HasOne(d => d.Nomer)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.NomerId)
                    .HasConstraintName("FK_12");
            });

            modelBuilder.Entity<Cleaning>(entity =>
            {
                entity.HasKey(e => e.IdCleaning)
                    .HasName("PK_21");

                entity.HasIndex(e => e.NomerId)
                    .HasName("FK_3");

                entity.HasIndex(e => e.SotrudnikId)
                    .HasName("FK_3_1");

                entity.Property(e => e.IdCleaning).HasColumnName("ID_Cleaning");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.NomerId).HasColumnName("Nomer_ID");

                entity.Property(e => e.SotrudnikId).HasColumnName("Sotrudnik_ID");

                entity.HasOne(d => d.Nomer)
                    .WithMany(p => p.Cleaning)
                    .HasForeignKey(d => d.NomerId)
                    .HasConstraintName("FK_9");

                entity.HasOne(d => d.Sotrudnik)
                    .WithMany(p => p.Cleaning)
                    .HasForeignKey(d => d.SotrudnikId)
                    .HasConstraintName("FK_13");
            });

            modelBuilder.Entity<CleaningEquipment>(entity =>
            {
                entity.HasKey(e => e.IdCleaningEquipment)
                    .HasName("PK_22");

                entity.ToTable("Cleaning_Equipment");

                entity.HasIndex(e => e.CleaningId)
                    .HasName("FK_3");

                entity.HasIndex(e => e.EquipmentId)
                    .HasName("FK_2");

                entity.Property(e => e.IdCleaningEquipment).HasColumnName("ID_Cleaning_Equipment");

                entity.Property(e => e.CleaningId).HasColumnName("Cleaning_ID");

                entity.Property(e => e.EquipmentId).HasColumnName("Equipment_ID");

                entity.HasOne(d => d.Cleaning)
                    .WithMany(p => p.CleaningEquipment)
                    .HasForeignKey(d => d.CleaningId)
                    .HasConstraintName("FK_12_1");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.CleaningEquipment)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK_6");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("PK_10");

                entity.Property(e => e.IdClient).HasColumnName("ID_Client");

                entity.Property(e => e.DateRozhdeniya)
                    .HasColumnName("Date_Rozhdeniya")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Otchestvo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasKey(e => e.IdDish)
                    .HasName("PK_9");

                entity.Property(e => e.IdDish).HasColumnName("ID_Dish");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DishFood>(entity =>
            {
                entity.HasKey(e => e.IdDishFood)
                    .HasName("PK_8");

                entity.ToTable("Dish_Food");

                entity.HasIndex(e => e.DishId)
                    .HasName("FK_3");

                entity.HasIndex(e => e.FoodId)
                    .HasName("FK_2");

                entity.Property(e => e.IdDishFood).HasColumnName("ID_Dish_Food");

                entity.Property(e => e.DishId).HasColumnName("Dish_ID");

                entity.Property(e => e.FoodId).HasColumnName("Food_ID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.DishFood)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_16");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.DishFood)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK_15");
            });

            modelBuilder.Entity<Doljnost>(entity =>
            {
                entity.HasKey(e => e.IdDoljnost)
                    .HasName("PK_7");

                entity.Property(e => e.IdDoljnost).HasColumnName("ID_Doljnost");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnType("decimal(36, 2)");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasKey(e => e.IdEquipment)
                    .HasName("PK_11");

                entity.HasIndex(e => e.SkladId)
                    .HasName("FK_2");

                entity.Property(e => e.IdEquipment).HasColumnName("ID_Equipment");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SkladId).HasColumnName("Sklad_ID");

                entity.HasOne(d => d.Sklad)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.SkladId)
                    .HasConstraintName("FK_5");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasKey(e => e.IdFood)
                    .HasName("PK_6");

                entity.HasIndex(e => e.FridgeId)
                    .HasName("FK_2");

                entity.Property(e => e.IdFood).HasColumnName("ID_Food");

                entity.Property(e => e.FridgeId).HasColumnName("Fridge_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fridge)
                    .WithMany(p => p.Food)
                    .HasForeignKey(d => d.FridgeId)
                    .HasConstraintName("FK_14");
            });

            modelBuilder.Entity<Fridge>(entity =>
            {
                entity.HasKey(e => e.IdFridge)
                    .HasName("PK_5");

                entity.Property(e => e.IdFridge).HasColumnName("ID_Fridge");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistorySotrudnik>(entity =>
            {
                entity.ToTable("History_Sotrudnik");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Operation)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SotrudnikId).HasColumnName("Sotrudnik_Id");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK_4");

                entity.Property(e => e.IdMenu).HasColumnName("ID_Menu");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MenuDate>(entity =>
            {
                entity.HasKey(e => e.IdMenuDate)
                    .HasName("PK_17");

                entity.ToTable("Menu_Date");

                entity.HasIndex(e => e.MenuId)
                    .HasName("FK_2");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_3");

                entity.Property(e => e.IdMenuDate).HasColumnName("ID_Menu_Date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MenuId).HasColumnName("Menu_ID");

                entity.Property(e => e.TypeId).HasColumnName("Type_ID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuDate)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_19");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.MenuDate)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_20");
            });

            modelBuilder.Entity<MenuDish>(entity =>
            {
                entity.HasKey(e => e.IdMenuDish)
                    .HasName("PK_19");

                entity.ToTable("Menu_Dish");

                entity.HasIndex(e => e.DishId)
                    .HasName("FK_2");

                entity.HasIndex(e => e.MenuId)
                    .HasName("FK_3");

                entity.Property(e => e.IdMenuDish).HasColumnName("ID_Menu_Dish");

                entity.Property(e => e.DishId).HasColumnName("Dish_ID");

                entity.Property(e => e.MenuId).HasColumnName("Menu_ID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.MenuDish)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_17");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuDish)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_18");
            });

            modelBuilder.Entity<Nomer>(entity =>
            {
                entity.HasKey(e => e.IdNomer)
                    .HasName("PK_16");

                entity.HasIndex(e => e.TypeId)
                    .HasName("FK_2");

                entity.Property(e => e.IdNomer).HasColumnName("ID_Nomer");

                entity.Property(e => e.TypeId).HasColumnName("Type_ID");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Nomer)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_8");
            });

            modelBuilder.Entity<PriemPitaniya>(entity =>
            {
                entity.HasKey(e => e.IdPriem)
                    .HasName("PK_18");

                entity.ToTable("Priem_Pitaniya");

                entity.HasIndex(e => e.MenuDateId)
                    .HasName("FK_3");

                entity.HasIndex(e => e.NomerId)
                    .HasName("FK_2");

                entity.Property(e => e.IdPriem).HasColumnName("ID_Priem");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.MenuDateId).HasColumnName("Menu_Date_ID");

                entity.Property(e => e.NomerId).HasColumnName("Nomer_ID");

                entity.HasOne(d => d.MenuDate)
                    .WithMany(p => p.PriemPitaniya)
                    .HasForeignKey(d => d.MenuDateId)
                    .HasConstraintName("FK_21_1");

                entity.HasOne(d => d.Nomer)
                    .WithMany(p => p.PriemPitaniya)
                    .HasForeignKey(d => d.NomerId)
                    .HasConstraintName("FK_21");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK_1");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sklad>(entity =>
            {
                entity.HasKey(e => e.IdSklad)
                    .HasName("PK_12");

                entity.Property(e => e.IdSklad).HasColumnName("ID_Sklad");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sotrudnik>(entity =>
            {
                entity.HasKey(e => e.IdSotrudnik)
                    .HasName("PK_2");

                entity.Property(e => e.IdSotrudnik).HasColumnName("ID_Sotrudnik");

                entity.Property(e => e.DateRozhdenia)
                    .HasColumnName("Date_Rozhdenia")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Otchestvo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SotrudnikDoljnost>(entity =>
            {
                entity.HasKey(e => e.IdSotrudnikDoljnost)
                    .HasName("PK_13");

                entity.ToTable("Sotrudnik_Doljnost");

                entity.HasIndex(e => e.DoljnostId)
                    .HasName("FK_2");

                entity.HasIndex(e => e.SotrudnikId)
                    .HasName("FK_3");

                entity.Property(e => e.IdSotrudnikDoljnost).HasColumnName("ID_Sotrudnik_Doljnost");

                entity.Property(e => e.DoljnostId).HasColumnName("Doljnost_ID");

                entity.Property(e => e.SotrudnikId).HasColumnName("Sotrudnik_ID");

                entity.HasOne(d => d.Doljnost)
                    .WithMany(p => p.SotrudnikDoljnost)
                    .HasForeignKey(d => d.DoljnostId)
                    .HasConstraintName("FK_3");

                entity.HasOne(d => d.Sotrudnik)
                    .WithMany(p => p.SotrudnikDoljnost)
                    .HasForeignKey(d => d.SotrudnikId)
                    .HasConstraintName("FK_4");
            });

            modelBuilder.Entity<TypeMeal>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_14");

                entity.ToTable("Type_Meal");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeNomer>(entity =>
            {
                entity.HasKey(e => e.IdType)
                    .HasName("PK_15");

                entity.ToTable("Type_Nomer");

                entity.Property(e => e.IdType).HasColumnName("ID_Type");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_3");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FK_2");

                entity.HasIndex(e => e.SotrudnikId)
                    .HasName("FK_3");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.SotrudnikId).HasColumnName("Sotrudnik_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_1");

                entity.HasOne(d => d.Sotrudnik)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.SotrudnikId)
                    .HasConstraintName("FK_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
