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

        modelBuilder.Entity<CompanyInfoModel>().HasData(CreateCompanyInfoModels());
        modelBuilder.Entity<LegalModel>().HasData(CreateLegalModels());
        modelBuilder.Entity<ContentModel>().HasData(CreateContentModels());
    }

    private IEnumerable<CompanyInfoModel> CreateCompanyInfoModels()
    {
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
                    Name = num.GetDescription(),
                    Content = ""
                }
            );
        }

        return companyModels;
    }

    private IEnumerable<LegalModel> CreateLegalModels()
    {
        var legalModels = new List<LegalModel>();

        return legalModels;
    }

    private LegalModel CreateLegalModel(string name)
    {
        return new LegalModel
        {
            Id = Guid.NewGuid(),
            LanguageCode = "tr",
            Name = name,
            Link = name.Linkify(),
            Content = ""
        };
    }

    private IEnumerable<ContentModel> CreateContentModels()
    {
        var contentModels = new List<ContentModel>();
        contentModels.Add(CreateContentModel(null, null, Guid.NewGuid(), "Ana Sayfa", 1));
        contentModels.Add(CreateContentModel(null, null, Guid.NewGuid(), "SSS", 2));
        contentModels.Add(CreateContentModel(null, null, Guid.NewGuid(), "İletişim", 10));

        return contentModels;
    }

    private ContentModel CreateContentModel(Guid? headerId, string? headerTitle, Guid id, string title, int sortOrder)
    {
        return new ContentModel
        {
            Id = id,
            LanguageCode = "tr",
            Name = title,
            Link = headerTitle.Linkify() + title.Linkify(),
            Content = null,
            IsSubContent = false,
            IsImageLibrary = false,
            IsVisibleOnIndex = false,
            IsFixed = true,
            IsCompleted = false,
            SortOrder = sortOrder,
            HeaderContentId = headerId
        };
    }
}