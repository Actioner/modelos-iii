using System;
using System.Collections.Generic;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Tasks.Commands.Utility;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.Commands.Promotion
{
    public class PromotionCommand : CommandBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PromotionType Type { get; set; }
        public IList<DayOfWeek> Days { get; set; }
        public bool Active { get; set; }
        public string Poster { get; set; }
        public FileUploadModel NewPoster { get; set; }

        public PromotionCommand()
        {
            NewPoster = new FileUploadModel
                             {
                                 Id = "NewPoster"
                             };
        }
    }
}