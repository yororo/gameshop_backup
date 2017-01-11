using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameShop.Authorization.Models 
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser 
    { 
        // /// <summary>
        // /// Get ID as Guid.
        // /// </summary>
        // public Guid Guid
        // {
        //     get
        //     {
        //         try
        //         {
        //             return Guid.Parse(Id);
        //         }
        //         catch(Exception ex)
        //         {
        //             throw new Exception("Unable to parse ID to type: Guid.", ex);
        //         }
        //     }
        // }

        // public virtual Profile Profile { get; set; }
        // public DateTime? CreatedDate { get; set; }
        // public DateTime? ModifiedDate { get; set; }
    }
}
