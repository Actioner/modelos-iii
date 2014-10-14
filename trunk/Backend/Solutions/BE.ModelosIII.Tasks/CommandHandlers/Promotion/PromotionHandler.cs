using System;
using System.IO;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure;
using BE.ModelosIII.Tasks.Commands.Promotion;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Promotion
{
    public abstract class PromotionHandler<T> : ICommandHandler<T>
        where T : PromotionCommand
    {
        protected IPromotionRepository PromotionRepository;
        protected IMappingEngine MappingEngine;

        protected PromotionHandler(
            IPromotionRepository promotionRepository, 
            IMappingEngine mappingEngine)
        {
            this.PromotionRepository = promotionRepository;
            this.MappingEngine = mappingEngine;
        }

        public virtual void Handle(T command)
        {
            var promotion = MapCommandToPromotion(command);

            if (!string.IsNullOrWhiteSpace(command.NewPoster.NewFile))
            {
                string sourcePath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.TempUploadFolder);
                string destPath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.PosterUploadFolder);
                string fileName = Path.GetFileName(command.NewPoster.NewFile);
                File.Move(sourcePath + @"\" + fileName, destPath + @"\" + fileName);

                promotion.Poster = "/" + BackendSettings.PosterUploadFolder + "/" + fileName;
                TryDelete(sourcePath + @"\" + fileName);
            }

            var result = PromotionRepository.SaveOrUpdate(promotion);
            
            command.Id = result.Id;
        }

        private bool TryDelete(string file)
        {
            try
            {
                File.Delete(file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    
        protected abstract Domain.Promotion MapCommandToPromotion(T command);
    }
}
