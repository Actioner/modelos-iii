using System.Collections.Generic;
using AutoMapper;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using BE.ModelosIII.Tasks.Commands.Multiplex.Screen;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using System.Web;
using BE.ModelosIII.Infrastructure;
using System.IO;
using System;

namespace BE.ModelosIII.Tasks.CommandHandlers.Multiplex.Screen
{
    public abstract class ScreenHandler<T> : ICommandHandler<T>
        where T : ScreenCommand
    {
        protected IScreenRepository ScreenRepository;
        protected IMappingEngine MappingEngine;

        protected ScreenHandler(
            IScreenRepository screenRepository, 
            IMappingEngine mappingEngine)
        {
            this.ScreenRepository = screenRepository;
            this.MappingEngine = mappingEngine;
        }

        public virtual void Handle(T command)
        {
            var screen = MapCommandToScreen(command);

            if (!string.IsNullOrWhiteSpace(command.LayoutFile.NewFile))
            {
                string sourcePath = HttpContext.Current.Server.MapPath("~/" + BackendSettings.TempUploadFolder);
                string fileName = Path.GetFileName(command.LayoutFile.NewFile);
                TryDelete(sourcePath + @"\" + fileName);
            }
            var result = ScreenRepository.SaveOrUpdate(screen);
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

        protected abstract Domain.Screen MapCommandToScreen(T command);
    }
}
