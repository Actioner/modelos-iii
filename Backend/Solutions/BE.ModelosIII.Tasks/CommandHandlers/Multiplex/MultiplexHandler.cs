using System;
using System.IO;
using System.Web;
using AutoMapper;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Infrastructure;
using BE.ModelosIII.Tasks.Commands.Multiplex;
using SharpArch.Domain.Commands;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex
{
    public abstract class MultiplexHandler<T> : ICommandHandler<T>
        where T : MultiplexCommand
    {
        protected IMultiplexRepository MultiplexRepository;
        protected IMappingEngine MappingEngine;

        protected MultiplexHandler(
            IMultiplexRepository multiplexRepository, 
            IMappingEngine mappingEngine)
        {
            this.MultiplexRepository = multiplexRepository;
            this.MappingEngine = mappingEngine;
        }

        public virtual void Handle(T command)
        {
            var multiplex = MapCommandToMultiplex(command);

            if (!string.IsNullOrWhiteSpace(command.NewPoster.NewFile))
            {
                string sourcePath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.TempUploadFolder);
                string destPath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.PosterUploadFolder);
                string fileName = Path.GetFileName(command.NewPoster.NewFile);
                File.Move(sourcePath + @"\" + fileName, destPath + @"\" + fileName);

                multiplex.Poster = "/" + BackendSettings.PosterUploadFolder + "/" + fileName;
                TryDelete(sourcePath + @"\" + fileName);
            }

            var result = MultiplexRepository.SaveOrUpdate(multiplex);
            
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

        protected abstract Domain.Multiplex MapCommandToMultiplex(T command);
    }
}
