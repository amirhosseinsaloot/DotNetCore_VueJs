using System.ComponentModel.DataAnnotations;

namespace Core.Enums;

public enum ApiResultBodyCode : byte
{
    [Display(Name = "Operation done successfully")]
    Success = 0,

    [Display(Name = "Server error occurred")]
    ServerError = 1,

    [Display(Name = "Invalid arguments")]
    BadRequest = 2,

    [Display(Name = "Not found")]
    NotFound = 3,

    [Display(Name = "Authentication error")]
    UnAuthorized = 4,

    [Display(Name = "ExpiredSecurityToken error")]
    ExpiredSecurityToken = 5,

    [Display(Name = "Forbidden error (User does not have permission)")]
    Forbidden = 6,

    [Display(Name = "Duplication error")]
    Duplication = 7,
}
