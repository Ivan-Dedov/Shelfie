namespace Shelfie.Api.Interactions.Models.ChangeStatus;

public enum BookStatus
{
    None = 0,

    Planning = 1,
    InProgress = 2,
    Finished = 3,

    Dropped = -1,
    Unknown = -100
}
