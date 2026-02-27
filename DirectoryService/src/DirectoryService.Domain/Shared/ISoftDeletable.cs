namespace DirectoryService.Domain.Shared;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
    DateTime? DeletionDate { get; }
}
