﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itrantion.Server.Models
{
    [Table("presentations")]
    public class PresentationModel
    {
        [Column][Key] public Guid Id { get; private set; } = Guid.NewGuid();
        [Column] public string Name { get; private set; } = string.Empty;
        [Column] public string Owner { get; private set; } = string.Empty;
        [Column] public DateTime Date { get; private set; } = DateTime.Now;
        [Column] public int Type { get; private set; } = 0;

        [NotMapped] public List<UserModel> connectedUsers = [];

        [NotMapped] public List<PermissionModel> permissions = [];

        private PresentationModel() { }

        public static (string? error, PresentationModel? instance) Create(string name, string owner, DateTime date, int type)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return new("Name is required", null);
            }

            if (string.IsNullOrEmpty(owner) || string.IsNullOrWhiteSpace(owner))
            {
                return new("Owner is required", null);
            }

            return new(null, new PresentationModel()
            {
                Owner = owner,
                Name = name,
                Date = date,
                Type = type
            });
        }
    }
}