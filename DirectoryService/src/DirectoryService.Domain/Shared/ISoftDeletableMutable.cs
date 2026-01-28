namespace DirectoryService.Domain.Shared;

public interface ISoftDeletableMutable : ISoftDeletable
{
    void MarkAsDeleted();
    void Restore();
}
