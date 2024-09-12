using Syncer.APIs.Models.Domain;

namespace Syncer.APIs.Persistence;

public class SyncerDbContext(DbContextOptions<SyncerDbContext> options) : DbContext(options)
{
    public const string ConnectionStringName = "SvcDbContext";

    public DbSet<Presentation> Presentations { get; set; }

    public DbSet<Emoji> Emojis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurePresentation(modelBuilder);

        ConfigureEmoji(modelBuilder);
    }

    private static void ConfigureEmoji(ModelBuilder modelBuilder)
    {
        var emoji = modelBuilder.Entity<Emoji>();

        emoji.
            HasKey(x => x.Code);

        emoji
            .Property(x => x.Code)
            .IsUnicode(false)
            .ValueGeneratedNever()
            .HasMaxLength(100);

        emoji
            .Property(x => x.ShortName)
            .IsUnicode(false)
            .HasMaxLength(100);
    }

    private static void ConfigurePresentation(ModelBuilder modelBuilder)
    {
        var presentation = modelBuilder.Entity<Presentation>();

        presentation
            .HasKey(x => x.Id);

        presentation
            .Property(x => x.Id)
            .IsUnicode(false)
            .ValueGeneratedNever();

        presentation
            .Property(x => x.Title)
            .HasMaxLength(300)
            .IsUnicode()
            .IsRequired();

        presentation
            .Property(x => x.Description)
            .HasMaxLength(2000)
            .IsUnicode()
            .IsRequired();

        presentation
            .Property(x => x.Speaker)
            .IsUnicode(false)
            .IsRequired();

        presentation.OwnsMany(x => x.Joiners, joinerBuilder =>
        {
            joinerBuilder.ToJson();
        });

        presentation
            .OwnsMany(x => x.Milestones, milestoneBuilder =>
            {
                milestoneBuilder.
                HasKey(x => x.Id);

                milestoneBuilder
                .Property(x => x.Status)
                .IsRequired();

                milestoneBuilder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode();

                milestoneBuilder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();

                milestoneBuilder
                .Property(x => x.PresentationId)
                .IsRequired();

                milestoneBuilder.OwnsMany(x => x.Emojis, emojisBuilder =>
                {
                    emojisBuilder.ToTable("MilestoneEmojis");
                });

                milestoneBuilder.OwnsMany(x => x.Reactions, reactionBuilder =>
                {
                    reactionBuilder.ToTable("MilestoneReactions");
                });
            });
    }
}
