namespace Api.Modules.Enums;

public enum CategoryErrorCode
{
    ParentRequired,
    CategoryNotFound,
    CategoryCannotBeSelfReferenced,
    CategoryGroupNotFound,
    GroupMismatch,
    GroupMaxCategoryExceeded
}