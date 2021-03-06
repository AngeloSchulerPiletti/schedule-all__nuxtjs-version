using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("users")]
    public class User
    {
        public User(string userName, string fullName, string email, string password, string walletAddress)
        {
            UserName = userName;
            FullName = fullName;
            Email = email;
            Password = password;
            WalletAddress = walletAddress;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("user_name")]
        public string UserName {get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("wallet_address")]
        public string WalletAddress { get; set; }
        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
