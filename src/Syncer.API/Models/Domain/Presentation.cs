﻿namespace Syncer.APIs.Models.Domain;

public class Presentation
{
    public required string Id { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string Speaker { get; set; }

    public PresentationStatus Status { get; set; }

    public ICollection<Milestone> Milestones { get; set; }
}

public enum PresentationStatus
{
    Created = 1,
    Present = 2,
    Finished = 3
}