using Backend.Datas.Enums;
using Backend.Misc;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Connection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
    }

    public DbSet<UserModel>? Users { get; set; }
    public DbSet<CompanyInfoModel>? CompanyInfos { get; set; }
    public DbSet<ContentModel>? Contents { get; set; }
    public DbSet<ImageLibraryModel>? Images { get; set; }
    public DbSet<FaqModel>? Faqs { get; set; }
    public DbSet<LegalModel>? Legals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>().HasData(
            new UserModel
            {
                Id = Guid.NewGuid(),
                Username = "Admin",
                Email = "info@mustafaogluinsaat.com",
                Password = Misc.AesEncryption.Encrypt("mstins2022"),
                Type = Datas.Enums.UserType.Admin,
                LanguageCode = "tr"
            }
        );

        var companyModels = new List<CompanyInfoModel>();
        foreach (CompanyInfoType num in Enum.GetValues(typeof(CompanyInfoType)))
        {
            if (num == CompanyInfoType.None)
                continue;
            companyModels.Add(
                new CompanyInfoModel
                {
                    Id = Guid.NewGuid(),
                    LanguageCode = "tr",
                    Type = num,
                    Content = ""
                }
            );

        }
        modelBuilder.Entity<CompanyInfoModel>().HasData(companyModels);

        var legalModels = new List<LegalModel>();
        foreach (LegalType num in Enum.GetValues(typeof(LegalType)))
        {
            if (num == LegalType.None)
                continue;
            legalModels.Add(
                new LegalModel
                {
                    Id = Guid.NewGuid(),
                    LanguageCode = "tr",
                    Type = num,
                    Name = num.GetDescription(),
                    Link = num.GetDescription().Linkify(),
                    Content = ""
                });

        }
        modelBuilder.Entity<LegalModel>().HasData(legalModels);

        modelBuilder.Entity<ContentModel>().HasData(CreateContentModels());
    }

    private IEnumerable<ContentModel> CreateContentModels()
    {
        var contentModels = new List<ContentModel>();
        contentModels.Add(CreateContentModel(null, null, Guid.Empty, ContentType.Home, "Ana Sayfa", 1));

        var headerId = Guid.NewGuid();
        var headerTitle = "Kurumsal";
        contentModels.Add(CreateContentModel(null, null, headerId, ContentType.AboutUs, headerTitle, 2));
        contentModels.Add(CreateContentModel(headerId, headerTitle, Guid.Empty, ContentType.AboutUs, "Misyon", 1));
        contentModels.Add(CreateContentModel(headerId, headerTitle, Guid.Empty, ContentType.AboutUs, "Vizyon", 2));
        contentModels.Add(CreateContentModel(headerId, headerTitle, Guid.Empty, ContentType.AboutUs, "Hakkımızda", 3));

        headerId = Guid.NewGuid();
        headerTitle = "Projeler";
        contentModels.Add(CreateContentModel(null, null, headerId, ContentType.Projects, headerTitle, 3));
        contentModels.Add(CreateContentModel(headerId, headerTitle, Guid.Empty, ContentType.Projects, "Devam Eden Projeler", 1));
        contentModels.Add(CreateContentModel(headerId, headerTitle, Guid.Empty, ContentType.Projects, "Tamamlanan Projeler", 2));

        contentModels.Add(CreateContentModel(null, null, Guid.Empty, ContentType.Services, "Hizmetler", 4));

        contentModels.Add(CreateContentModel(null, null, Guid.Empty, ContentType.Contact, "İletişim", 10));

        return contentModels;
    }

    private ContentModel CreateContentModel(Guid? headerId, string? headerTitle, Guid id, ContentType type, string title, int sortOrder)
    {
        return new ContentModel
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            LanguageCode = "tr",
            Name = title,
            Link = headerTitle.Linkify() + title.Linkify(),
            Type = type,
            Content = null,
            ImageLibrary = false,
            SortOrder = sortOrder,
            VisibleOnMain = false,
            IsFixed = true,
            HeaderContentId = headerId
        };
    }
}