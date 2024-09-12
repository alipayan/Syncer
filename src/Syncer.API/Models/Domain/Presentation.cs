namespace Syncer.APIs.Models.Domain;

public class Presentation
{
    public required string Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Speaker { get; set; }

    public PresentationStatus Status { get; set; }

    public static Presentation Create(string id, string title, string description, string speaker)
        => new Presentation
        {
            Id = id,
            Title = title,
            Description = description,
            Speaker = speaker,
            Status = PresentationStatus.Present
        };

    public ICollection<PresentationJoiner> Joiners { get; set; } = null!;

    public ICollection<Milestone> Milestones { get; set; } = null!;
}

public record PresentationJoiner(string Username);

public enum PresentationStatus
{
    Created = 1,
    Present = 2,
    Finished = 3
}